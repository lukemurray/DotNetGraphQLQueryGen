using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using RazorLight;

namespace dotnet_gqlgen
{
    public class Program
    {
        [Argument(0, Description = "Path to the GraphQL schema file or a GraphQL introspection endpoint")]
        [Required]
        public string Source { get; }

        [Option(LongName = "header", ShortName = "h", Description = "Headers to pass to GraphQL introspection endpoint. Use \"Authorization=Bearer eyJraWQ,X-API-Key=abc,...\"")]
        public string HeaderValues { get; }

        [Option(LongName = "namespace", ShortName = "n", Description = "Namespace to generate code under")]
        public string Namespace { get; } = "Generated";

        [Option(LongName = "client_class_name", ShortName = "c", Description = "Name for the client class")]
        public string ClientClassName { get; } = "GraphQLClient";
        [Option(LongName = "scalar_mapping", ShortName = "m", Description = "Map of custom schema scalar types to dotnet types. Use \"GqlType=DotNetClassName,ID=Guid,...\"")]
        public string ScalarMapping { get; }
        [Option(LongName = "output", ShortName = "o", Description = "Output directory")]
        public string OutputDir { get; } = "output";

        public Dictionary<string, string> dotnetToGqlTypeMappings = new Dictionary<string, string> {
            {"string", "String"},
            {"String", "String"},
            {"int", "Int!"},
            {"Int32", "Int!"},
            {"double", "Float!"},
            {"bool", "Boolean!"},
        };

        public static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

        private async void OnExecute()
        {
            try
            {
                Uri uriResult;
                bool isGraphQlEndpoint = Uri.TryCreate(Source, UriKind.Absolute, out uriResult)
                                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                string schemaText = null;
                bool isIntroSpectionFile = false;

                if (isGraphQlEndpoint)
                {
                    Console.WriteLine($"Loading from {Source}...");
                    using (var httpClient = new HttpClient())
                    {
                        foreach (var header in SplitMultiValueArgument(HeaderValues))
                        {
                            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }

                        Dictionary<string, string> request = new Dictionary<string, string>();
                        request["query"] = IntroSpectionQuery.Query;
                        request["operationName"] = "IntrospectionQuery";

                        var response = httpClient
                            .PostAsync(Source,
                            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                        schemaText = await response.Content.ReadAsStringAsync();
                        isIntroSpectionFile = true;
                    }
                }
                else
                {
                    Console.WriteLine($"Loading {Source}...");
                    schemaText = File.ReadAllText(Source);
                    isIntroSpectionFile = Path.GetExtension(Source).Equals(".json", StringComparison.OrdinalIgnoreCase);
                }

                var mappings = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ScalarMapping))
                {
                    SplitMultiValueArgument(ScalarMapping).ToList().ForEach(i => {
                        dotnetToGqlTypeMappings[i.Value] = i.Key;
                        mappings[i.Key] = i.Value;
                    });
                }

                // parse into AST
                var typeInfo = !isIntroSpectionFile ?
                    SchemaCompiler.Compile(schemaText, mappings) :
                    IntrospectionCompiler.Compile(schemaText, mappings);

                Console.WriteLine($"Generating types in namespace {Namespace}, outputting to {ClientClassName}.cs");

                // pass the schema to the template
                var engine = new RazorLightEngineBuilder()
                    .UseEmbeddedResourcesProject(typeof(Program))
                    .UseMemoryCachingProvider()
                    .Build();

                var allTypes = typeInfo.Types.Concat(typeInfo.Inputs).ToDictionary(k => k.Key, v => v.Value);

                string resultTypes = await engine.CompileRenderAsync("resultTypes.cshtml", new
                {
                    Namespace = Namespace,
                    SchemaFile = Source,
                    Types = allTypes,
                    Enums = typeInfo.Enums,
                    Mutation = typeInfo.Mutation,
                    CmdArgs = $"-n {Namespace} -c {ClientClassName} -m {ScalarMapping}"
                });
                Directory.CreateDirectory(OutputDir);
                File.WriteAllText($"{OutputDir}/GeneratedResultTypes.cs", resultTypes);

                string queryTypes = await engine.CompileRenderAsync("queryTypes.cshtml", new
                {
                    Namespace = Namespace,
                    SchemaFile = Source,
                    Types = allTypes,
                    Mutation = typeInfo.Mutation,
                    CmdArgs = $"-n {Namespace} -c {ClientClassName} -m {ScalarMapping}"
                });
                Directory.CreateDirectory(OutputDir);
                File.WriteAllText($"{OutputDir}/GeneratedQueryTypes.cs", queryTypes);

                resultTypes = await engine.CompileRenderAsync("client.cshtml", new
                {
                    Namespace = Namespace,
                    SchemaFile = Source,
                    Query = typeInfo.Query,
                    Mutation = typeInfo.Mutation,
                    ClientClassName = ClientClassName,
                    Mappings = dotnetToGqlTypeMappings
                });
                File.WriteAllText($"{OutputDir}/{ClientClassName}.cs", resultTypes);

                Console.WriteLine($"Done.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
        }

        /// <summary>
        /// Splits an argument value like "value1=v1;value2=v2" into a dictionary.
        /// </summary>
        /// <remarks>Very simple splitter. Eg can't handle semi-colon's or equal signs in values</remarks>
        private Dictionary<string, string> SplitMultiValueArgument(string arg)
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
