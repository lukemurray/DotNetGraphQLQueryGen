using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RazorLight;

namespace dotnet_gqlgen
{
    public class GeneratorOptions
    {
        public string Source { get; set; }
        public string HeaderValues { get; set; }
        public string Namespace { get; set; } = "Generated";
        public string ClientClassName { get; set; } = "GraphQLClient";
        public string ScalarMapping { get; set; }
        public string OutputDir { get; set; } = "output";
        public string Usings { get; set; } = "";
        public bool NoGeneratedTimestamp { get; set; }
        public bool ConvertToUnixLineEnding { get; set; } = true;
    }
        
    public class ModelType
    {
        public string Namespace { get; set; }
        public string SchemaFile { get; set; }
        public Dictionary<string, TypeInfo> Types { get; set; }
        public Dictionary<string, List<EnumInfo>> Enums { get; set; }
        public TypeInfo Mutation { get; set; }
        public string CmdArgs { get; set; }
        public string Usings { get; set; }
        public bool NoGeneratedTimestamp { get; set; }
    }

    public static class Generator
    {
        public static async Task Generate(GeneratorOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Source)) throw new ArgumentException($"{nameof(options.Source)} is required");

            var dotnetToGqlTypeMappings = new Dictionary<string, string>
            {
                { "string", "String" },
                { "String", "String" },
                { "int", "Int!" },
                { "Int32", "Int!" },
                { "double", "Float!" },
                { "bool", "Boolean!" },
            };

            Uri uriResult;
            bool isGraphQlEndpoint = Uri.TryCreate(options.Source, UriKind.Absolute, out uriResult)
                                     && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            string schemaText = null;
            bool isIntroSpectionFile = false;

            if (isGraphQlEndpoint)
            {
                Console.WriteLine($"Loading from {options.Source}...");
                using (var httpClient = new HttpClient())
                {
                    foreach (var header in SplitMultiValueArgument(options.HeaderValues))
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }

                    Dictionary<string, string> request = new Dictionary<string, string>();
                    request["query"] = IntroSpectionQuery.Query;
                    request["operationName"] = "IntrospectionQuery";

                    var response = httpClient
                        .PostAsync(options.Source,
                            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                    schemaText = await response.Content.ReadAsStringAsync();
                    isIntroSpectionFile = true;
                }
            }
            else
            {
                Console.WriteLine($"Loading {options.Source}...");
                schemaText = await File.ReadAllTextAsync(options.Source);
                isIntroSpectionFile = Path.GetExtension(options.Source).Equals(".json", StringComparison.OrdinalIgnoreCase);
            }

            var mappings = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(options.ScalarMapping))
            {
                SplitMultiValueArgument(options.ScalarMapping).ToList().ForEach(i =>
                {
                    dotnetToGqlTypeMappings[i.Value] = i.Key;
                    mappings[i.Key] = i.Value;
                });
            }

            // parse into AST
            var typeInfo = !isIntroSpectionFile ? SchemaCompiler.Compile(schemaText, mappings) : IntrospectionCompiler.Compile(schemaText, mappings);

            Console.WriteLine($"Generating types in namespace {options.Namespace}, outputting to {options.ClientClassName}.cs");

            var rootType = typeof(Generator);

            // pass the schema to the template
            var engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(rootType)
                .UseMemoryCachingProvider()
                .Build();

            var allTypes = typeInfo.Types.Concat(typeInfo.Inputs).ToDictionary(k => k.Key, v => v.Value);

            string resultTypes = await engine.CompileRenderAsync("resultTypes.cshtml", new ModelType
            {
                Namespace = options.Namespace,
                SchemaFile = options.Source,
                Types = allTypes,
                Enums = typeInfo.Enums,
                Mutation = typeInfo.Mutation,
                CmdArgs = $"-n {options.Namespace} -c {options.ClientClassName} -m {options.ScalarMapping} -u {options.Usings.Replace("\n", "\\n")}",
                Usings = options.Usings,
                NoGeneratedTimestamp = options.NoGeneratedTimestamp
            });
            Directory.CreateDirectory(options.OutputDir);
            await NormalizeAndWriteIfChanged($"{options.OutputDir}/GeneratedResultTypes.cs", resultTypes, options.ConvertToUnixLineEnding);

            string queryTypes = await engine.CompileRenderAsync("queryTypes.cshtml", new ModelType
            {
                Namespace = options.Namespace,
                SchemaFile = options.Source,
                Types = allTypes,
                Mutation = typeInfo.Mutation,
                CmdArgs = $"-n {options.Namespace} -c {options.ClientClassName} -m {options.ScalarMapping} -u {options.Usings.Replace("\n", "\\n")}",
                Usings = options.Usings,
                NoGeneratedTimestamp = options.NoGeneratedTimestamp
            });
            Directory.CreateDirectory(options.OutputDir);
            await NormalizeAndWriteIfChanged($"{options.OutputDir}/GeneratedQueryTypes.cs", queryTypes, options.ConvertToUnixLineEnding);

            resultTypes = await engine.CompileRenderAsync("client.cshtml", new
            {
                Namespace = options.Namespace,
                SchemaFile = options.Source,
                Query = typeInfo.Query,
                Mutation = typeInfo.Mutation,
                ClientClassName = options.ClientClassName,
                Mappings = dotnetToGqlTypeMappings,
                CmdArgs = $"-n {options.Namespace} -c {options.ClientClassName} -m {options.ScalarMapping}",
                options.NoGeneratedTimestamp
            });
            await NormalizeAndWriteIfChanged($"{options.OutputDir}/{options.ClientClassName}.cs", resultTypes, options.ConvertToUnixLineEnding);

            await WriteResourceToFile(rootType, "BaseGraphQLClient.cs", $"{options.OutputDir}/BaseGraphQLClient.cs", options.ConvertToUnixLineEnding);
            await WriteResourceToFile(rootType, "GqlFieldNameAttribute.cs", $"{options.OutputDir}/GqlFieldNameAttribute.cs", options.ConvertToUnixLineEnding);
            
            Console.WriteLine($"Done.");
        }

        private static async Task NormalizeAndWriteIfChanged(string file, string text, bool convertToUnixLineEnding)
        {
            var normalizedText = convertToUnixLineEnding ? text.Replace("\r\n", "\n") : text;
            if (File.Exists(file) && string.Equals(await File.ReadAllTextAsync(file), normalizedText)) return;
            
            await File.WriteAllTextAsync(file, normalizedText);
        }
        
        private static async Task WriteResourceToFile(Type rootType, string resourceName, string outputLocation, bool convertToUnixLineEnding) 
        {
            var assembly = rootType.GetTypeInfo().Assembly;
            await using var resourceStream = assembly.GetManifestResourceStream($"{rootType.Namespace}.{resourceName}")!;
            using var streamReader = new StreamReader(resourceStream);

            var text = await streamReader.ReadToEndAsync();
            await NormalizeAndWriteIfChanged(outputLocation, text, convertToUnixLineEnding);
        }

        /// <summary>
        /// Splits an argument value like "value1=v1;value2=v2" into a dictionary.
        /// </summary>
        /// <remarks>Very simple splitter. Eg can't handle semi-colon's or equal signs in values</remarks>
        private static Dictionary<string, string> SplitMultiValueArgument(string arg)
        {
            if (string.IsNullOrEmpty(arg))
            {
                return new Dictionary<string, string>();
            }

            return arg
                .Split(';')
                .Select(h => h.Split('='))
                .Where(hs => hs.Length >= 2)
                .ToDictionary(key => key[0], value => value[1]);
        }
    }
}