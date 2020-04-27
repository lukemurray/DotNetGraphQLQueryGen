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
        [Argument(0, Description = "Path the the GraphQL schema file")]
        [Required]
        public string Source { get; }

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
                    using(var httpClient = new HttpClient())
                    {

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
                    ScalarMapping.Split(',').Select(s => s.Split('=')).ToList().ForEach(i => {
                        dotnetToGqlTypeMappings[i[1]] = i[0];
                        mappings[i[0]] = i[1];
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

                string result = await engine.CompileRenderAsync("types.cshtml", new
                {
                    Namespace = Namespace,
                    SchemaFile = Source,
                    Types = allTypes,
                    Enums = typeInfo.Enums,
                    Mutation = typeInfo.Mutation,
                    CmdArgs = $"-n {Namespace} -c {ClientClassName} -m {ScalarMapping}"
                });
                Directory.CreateDirectory(OutputDir);
                File.WriteAllText($"{OutputDir}/GeneratedTypes.cs", result);

                result = await engine.CompileRenderAsync("client.cshtml", new
                {
                    Namespace = Namespace,
                    SchemaFile = Source,
                    Query = typeInfo.Query,
                    Mutation = typeInfo.Mutation,
                    ClientClassName = ClientClassName,
                    Mappings = dotnetToGqlTypeMappings
                });
                File.WriteAllText($"{OutputDir}/{ClientClassName}.cs", result);

                Console.WriteLine($"Done.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
        }
    }
}
