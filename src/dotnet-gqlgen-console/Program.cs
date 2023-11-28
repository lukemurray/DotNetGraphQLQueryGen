using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

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
        
        [Option(LongName = "usings", ShortName = "u", Description = "Extra using statements to add to generated code.")]
        public string Usings { get; } = "";
        
        [Option(LongName = "no_generated_timestamp", ShortName = "nt", Description = "Don't add 'Generated on abc from xyz' in generated files")]
        public bool NoGeneratedTimestamp { get; }
        
        [Option(LongName = "unix", ShortName = "un", Description = "Convert windows endings to unix")]
        public bool ConvertToUnixLineEnding { get; }

        public static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

        private async Task OnExecute()
        {
            try
            {
                await Generator.Generate(new()
                {
                    Source = Source,
                    HeaderValues = HeaderValues,
                    Namespace = Namespace,
                    ClientClassName = ClientClassName,
                    ScalarMapping = ScalarMapping,
                    OutputDir = OutputDir,
                    Usings = Usings,
                    NoGeneratedTimestamp = NoGeneratedTimestamp,
                    ConvertToUnixLineEnding = ConvertToUnixLineEnding
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
    }
}