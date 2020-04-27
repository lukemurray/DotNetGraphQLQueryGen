using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace dotnet_gqlgen
{
    public enum OperationType
    {
        Query,
        Mutation,
        // Subscription // Doesn't seem to be supported in SchemaInfo
    }

    /// <summary>
    /// Transforms introspection results to <see cref="SchemaInfo"/>
    /// For more information about introspection files, see <seealso cref="https://graphql.org/learn/introspection/"/>
    /// </summary>
    public class IntrospectionCompiler
    {
        /// <summary>
        /// Filters out any item that matches the below regex
        /// Note: type names starting with __ are introspection related queries
        /// </summary>
        private readonly string omitTypeRegex = "^__|CacheControlScope";

        public static SchemaInfo Compile(string introSpectionText, Dictionary<string, string> typeMappings = null)
        {
            var root = JObject.Parse(introSpectionText);
            return new IntrospectionCompiler().ParseSchema(root, typeMappings);
        }

        private SchemaInfo ParseSchema(JToken root, Dictionary<string, string> typeMappings)
        {
            var schemaInfo = new SchemaInfo(typeMappings);

            var schema = root?["data"]?["__schema"];
            if (schema != null)
            {
                // Extract operation types from the schema and add to the schema info
                AddOperationTypes(schemaInfo, schema);
                AddTypes(schemaInfo, schema);
            }
            return schemaInfo;
        }

        private void AddOperationTypes(SchemaInfo schemaInfo, JToken schema)
        {
            // Transform operation types to fields
            var operationTypeFields = Enum.GetNames(typeof(OperationType))
                .Select(ot => char.ToLowerInvariant(ot[0]) + ot.Substring(1))
                .Select(otGql => new { otGql, typeObj = schema[$"{otGql}Type"] })
                .Where(res => res.typeObj != null)
                .Select(res => new Field(schemaInfo) { Name = res.otGql, TypeName = res.typeObj.ReadName() });

            schemaInfo.Schema.AddRange(operationTypeFields);
        }

        private void AddTypes(SchemaInfo schemaInfo, JToken schema)
        {
            JArray allTypes = schema["types"] as JArray;
            Regex omitTypeCheck = !string.IsNullOrEmpty(omitTypeRegex) ?
                new Regex(omitTypeRegex, RegexOptions.Compiled) : null;

            // Get a filtered list of types
            IEnumerable<JToken> typesToParse = omitTypeCheck != null ?
                allTypes.Where(type => !omitTypeCheck.IsMatch(type.ReadName())) :
                allTypes;

            foreach (var type in typesToParse)
            {
                var name = type.ReadName();
                var kind = type.ReadKind();

                switch (kind)
                {
                    case "ENUM":
                        schemaInfo.Enums.Add(name, type["enumValues"].Select(e => e.ReadName()).ToList());
                        break;

                    case "SCALAR":
                        if (!schemaInfo.gqlToDotnetTypeMappings.ContainsKey(name))
                        {
                            Console.WriteLine($"WARNING: Scalar type '{name}' not found in mappings");
                        }
                        break;

                    case "INPUT_OBJECT":
                        {
                            var inputFields = type["inputFields"].Select(i => GetField(schemaInfo, i));
                            var typeInfo = new TypeInfo(inputFields, name, type.ReadDescription(), true);

                            schemaInfo.Inputs.Add(name, typeInfo);
                        }
                        break;

                    case "OBJECT":
                        {
                            var fields = type["fields"].Select(i => GetField(schemaInfo, i));
                            var typeInfo = new TypeInfo(fields, name, type.ReadDescription());
                            schemaInfo.Types.Add(name, typeInfo);
                        }
                        break;

                    default:
                        Console.WriteLine($"Warning, no handler for '{kind}'");
                        break;
                }
            }
        }

        private Field GetField(SchemaInfo schemaInfo, JToken fieldToken)
        {
            var typeToken = fieldToken["type"];
            var argsToken = fieldToken["args"] as JArray;
            return new Field(schemaInfo)
            {
                Args = GetArgs(schemaInfo, argsToken),
                Name = fieldToken.ReadName(),
                Description = fieldToken.ReadDescription(),
                TypeName = GetValueType(typeToken),
                IsNonNullable = IsNonNullable(typeToken),
                IsArray = IsArray(typeToken)
            };
        }

        private List<Arg> GetArgs(SchemaInfo schemaInfo, JArray argsToken)
        {
            var argsList = new List<Arg>();
            if (argsToken != null && argsToken.Count > 0)
            {
                argsList.AddRange(argsToken.Select(at => new Arg(schemaInfo)
                {
                    Name = at.ReadName(),
                    Description = at.ReadDescription(),
                    TypeName = GetValueType(at["type"]),
                    IsNonNullable = IsNonNullable(at["type"]),
                    IsArray = IsArray(at["type"]),
                    Required = IsNonNullable(at["type"])
                }));
            }
            return argsList;
        }

        private string GetValueType(JToken typeToken)
        {
            var ofType = typeToken["ofType"];
            return (ofType != null && ofType.HasValues) ?
                GetValueType(ofType) : typeToken.ReadName();
        }

        private bool IsNonNullable(JToken typeToken) => typeToken.ReadKind() == "NON_NULL";

        private bool IsArray(JToken typeToken) => typeToken.ReadKind() == "LIST";
    }

    internal static class IntroSpectionExtensions
    {
        public static string ReadString(this JToken token, string fieldName) => (string)token[fieldName];

        public static string ReadName(this JToken token) => token.ReadString("name");

        public static string ReadKind(this JToken token) => token.ReadString("kind");

        public static string ReadDescription(this JToken token) => token.ReadString("description")?.Trim();
    }
}