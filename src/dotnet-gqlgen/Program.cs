using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using RazorLight;

namespace dotnet_gqlgen
{
    public class Program
    {
        [Argument(0, Description = "Path the the GraphQL schema file")]
        [Required]
        public string SchemaFile { get; }

        [Option(LongName = "namespace", ShortName = "n", Description = "Namespace to generate code under")]
        public string Namespace { get; } = "Generated";

        [Option(LongName = "client_class_name", ShortName = "c", Description = "Name for the client class")]
        public string ClientClassName { get; } = "GraphQLClient";
        [Option(LongName = "scalar_mapping", ShortName = "m", Description = "Map of custom schema scalar types to dotnet types. Use \"GqlType=DotNetClassName,ID=Guid,...\"")]
        public string ScalarMapping { get; }
        [Option(LongName = "output", ShortName = "o", Description = "Output directory")]
        public string OutputDir { get; } = "output";

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private async void OnExecute()
        {
            try
            {
                Console.WriteLine($"Loading {SchemaFile}...");
                var schemaText = File.ReadAllText(SchemaFile);

                var mappings = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ScalarMapping))
                {
                    mappings = ScalarMapping.Split(',').Select(s => s.Split('=')).ToDictionary(k => k[0], v => v[1]);
                }

                // parse into AST
                var typeInfo = SchemaCompiler.Compile(schemaText, mappings);

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
                    SchemaFile = SchemaFile,
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
                    SchemaFile = SchemaFile,
                    Query = typeInfo.Query,
                    Mutation = typeInfo.Mutation,
                    ClientClassName = ClientClassName,
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
