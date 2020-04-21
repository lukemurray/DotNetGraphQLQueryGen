using System.IO;
using System.Linq;
using dotnet_gqlgen;
using Xunit;

namespace DotNetGraphQLQueryGen.Tests
{
    public class VisitorTests
    {
        [Fact]
        public void TestSchemaQueryRequired()
        {
            Assert.Throws<SchemaException>(() => SchemaCompiler.Compile("schema { mutation: Mutation }"));
        }

        [Fact]
        public void TestSchemaQueryType()
        {
            var results = SchemaCompiler.Compile(File.ReadAllText("../../../schema.graphql"));
            Assert.Equal(2, results.Schema.Count);
            Assert.Equal(8, results.Types.Count);
            Assert.Equal(2, results.Inputs.Count);
            var queryTypeName = results.Schema.First(s => s.Name == "query").TypeName;

            var queryType = results.Types[queryTypeName];
            Assert.Equal(10, queryType.Fields.Count);
            Assert.Equal("actors", queryType.Fields.ElementAt(1).Name);
            Assert.Equal("Person", queryType.Fields.ElementAt(1).TypeName);
            Assert.True(queryType.Fields.ElementAt(1).IsArray);
            Assert.Equal("person", queryType.Fields.ElementAt(7).Name);
            Assert.Equal("Person", queryType.Fields.ElementAt(7).TypeName);
            Assert.False(queryType.Fields.ElementAt(7).IsArray);
            Assert.Single(queryType.Fields.ElementAt(7).Args);
            Assert.Equal("id", queryType.Fields.ElementAt(7).Args.First().Name);
            Assert.Equal("Int", queryType.Fields.ElementAt(7).Args.First().TypeName);
            Assert.True(queryType.Fields.ElementAt(7).Args.First().Required);
        }

        [Fact]
        public void TestSchemaMutationType()
        {
            var results = SchemaCompiler.Compile(File.ReadAllText("../../../schema.graphql"));
            var mutationTypeName = results.Schema.First(s => s.Name == "mutation").TypeName;

            var mutType = results.Types[mutationTypeName];
            Assert.Equal(3, mutType.Fields.Count);
            Assert.Equal("addMovie", mutType.Fields.ElementAt(0).Name);
            Assert.Equal("Add a new Movie object", mutType.Fields.ElementAt(0).Description);
            Assert.Equal("Movie", mutType.Fields.ElementAt(0).TypeName);
            Assert.False(mutType.Fields.ElementAt(0).IsArray);

            Assert.Equal(5, mutType.Fields.ElementAt(0).Args.Count);
            Assert.Equal("name", mutType.Fields.ElementAt(0).Args.ElementAt(0).Name);
            Assert.True(mutType.Fields.ElementAt(0).Args.ElementAt(0).Required);
            Assert.Equal("String", mutType.Fields.ElementAt(0).Args.ElementAt(0).TypeName);
            Assert.Equal("rating", mutType.Fields.ElementAt(0).Args.ElementAt(1).Name);
            Assert.Equal("Float", mutType.Fields.ElementAt(0).Args.ElementAt(1).TypeName);
            Assert.Equal("details", mutType.Fields.ElementAt(0).Args.ElementAt(2).Name);
            Assert.Equal("Detail", mutType.Fields.ElementAt(0).Args.ElementAt(2).TypeName);
            Assert.True(mutType.Fields.ElementAt(0).Args.ElementAt(2).IsArray);
            Assert.Equal("released", mutType.Fields.ElementAt(0).Args.ElementAt(4).Name);
            Assert.Equal("Date", mutType.Fields.ElementAt(0).Args.ElementAt(4).TypeName);
            Assert.False(mutType.Fields.ElementAt(0).Args.ElementAt(4).IsArray);
        }

        [Fact]
        public void TestSchemaTypeDef()
        {
            var results = SchemaCompiler.Compile(File.ReadAllText("../../../schema.graphql"));
            Assert.Equal(2, results.Schema.Count);
            Assert.Equal(8, results.Types.Count);
            Assert.Equal(2, results.Inputs.Count);
            var queryTypeName = results.Schema.First(s => s.Name == "query").TypeName;
            var mutationTypeName = results.Schema.First(s => s.Name == "mutation").TypeName;

            var typeDef = results.Types["Movie"];
            Assert.Equal("This is a movie entity", typeDef.Description);
            Assert.Equal(10, typeDef.Fields.Count);
            Assert.Equal("id", typeDef.Fields.ElementAt(0).Name);
            Assert.Equal("String", typeDef.Fields.ElementAt(1).TypeName);
            Assert.False(typeDef.Fields.ElementAt(1).IsArray);
            Assert.Equal("actors", typeDef.Fields.ElementAt(4).Name);
            Assert.Equal("Person", typeDef.Fields.ElementAt(4).TypeName);
            Assert.True(typeDef.Fields.ElementAt(4).IsArray);
        }
    }
}