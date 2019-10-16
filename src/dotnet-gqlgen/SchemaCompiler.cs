using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using GraphQLSchema.Grammer;

namespace dotnet_gqlgen
{
    public static class SchemaCompiler
    {
        public static SchemaInfo Compile(string schemaText, Dictionary<string, string> typeMappings = null)
        {
            try
            {
                var stream = new AntlrInputStream(schemaText);
                var lexer = new GraphQLSchemaLexer(stream);
                var tokens = new CommonTokenStream(lexer);
                var parser = new GraphQLSchemaParser(tokens);
                parser.BuildParseTree = true;
                parser.ErrorHandler = new BailErrorStrategy();
                var tree = parser.schema();
                var visitor = new SchemaVisitor(typeMappings);
                // visit each node. it will return a linq expression for each entity requested
                visitor.Visit(tree);

                if (visitor.SchemaInfo.Schema == null || !visitor.SchemaInfo.Schema.Any(f => f.Name == "query"))
                {
                    throw new SchemaException("A schema definition is required and must define the query type e.g. \"schema { query: MyQueryType }\"");
                }

                return visitor.SchemaInfo;
            }
            catch (ParseCanceledException pce)
            {
                if (pce.InnerException != null)
                {
                    if (pce.InnerException is NoViableAltException)
                    {
                        var nve = (NoViableAltException)pce.InnerException;
                        throw new SchemaException($"Error: line {nve.OffendingToken.Line}:{nve.OffendingToken.Column} no viable alternative at input '{nve.OffendingToken.Text}'");
                    }
                    else if (pce.InnerException is InputMismatchException)
                    {
                        var ime = (InputMismatchException)pce.InnerException;
                        var expecting = string.Join(", ", ime.GetExpectedTokens());
                        throw new SchemaException($"Error: line {ime.OffendingToken.Line}:{ime.OffendingToken.Column} extraneous input '{ime.OffendingToken.Text}' expecting {expecting}");
                    }
                    throw new SchemaException(pce.InnerException.Message);
                }
                throw new SchemaException(pce.Message);
            }
        }
    }
}