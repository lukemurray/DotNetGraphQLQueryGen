using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNetGqlClient;
using Generated;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace CoreData.Model.Tests
{
    public class TestClient : TestHttpClient
    {
        internal QueryRequest MakeQuery<TReturn>(Expression<Func<RootQuery, TReturn>> p, bool mutation = false)
        {
            return base.MakeQuery(p, "TestQuery", mutation);
        }
        internal QueryRequest MakeMutation<TReturn>(Expression<Func<Mutation, TReturn>> p, bool mutation = false)
        {
            return base.MakeQuery(p, "TestQuery", mutation);
        }
    }

    public class TestTypedQuery
    {
        public TestTypedQuery(ITestOutputHelper testOutputHelper)
        {
        }

        [Fact]
        public void SimpleArgs()
        {
            var client = new TestClient();
            var query = client.MakeQuery(q => new {
                Movies = q.Movies(s => new {
                    s.Id,
                }),
            });
            Assert.Equal($@"query TestQuery  {{
Movies: movies {{
Id: id
}}
}}", query.Query, ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void SimpleQuery()
        {
            var client = new TestClient();
            var query = client.MakeQuery(q => new {
                Actors = q.Actors(s => new {
                    s.Id,
                    DirectorOf = s.DirectorOf(),
                }),
            });
            Assert.Equal($@"query TestQuery  {{
Actors: actors {{
Id: id
DirectorOf: directorOf {{
Id: id
Name: name
Genre: genre
Released: released
DirectorId: directorId
Rating: rating
Type: type
}}
}}
}}", query.Query, ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void MethodWithScalarReturn()
        {
            var client = new TestClient();
            var query = client.MakeQuery(_ => new {
                displayName = _.GetDisplayName(1)
            });
            Assert.Equal($@"query TestQuery  {{
displayName: getDisplayName(id: 1)
}}", query.Query, ignoreLineEndingDifferences: true);
        }


        [Fact]
        public void TypedClass()
        {
            var client = new TestClient();
            var query = client.MakeQuery(q => new MyResult
            {
                Movies = q.Movies(s => new MovieResult
                {
                    Id = s.Id,
                }),
            });
            Assert.Equal($@"query TestQuery  {{
Movies: movies {{
Id: id
}}
}}", query.Query, ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void TestMutationWithDate()
        {
            var client = new TestClient();
            var query = client.MakeMutation(q => new
            {
                Movie = q.AddMovie("movie", 5.5, null, null, new DateTime(2019, 10, 30, 17, 55, 23), s => new
                {
                    s.Id,
                }),
            }, true);
            Assert.Equal($@"mutation TestQuery  {{
Movie: addMovie(name: ""movie"", rating: 5.5, released: ""2019-10-30T17:55:23.0000000"") {{
Id: id
}}
}}", query.Query, ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void TestArrayArg()
        {
            var client = new TestClient();
            var idList = new List<int?> { 1, 2, 5 };
            var query = client.MakeQuery(q => new
            {
                Movies = q.MoviesByIds(idList, s => new
                {
                    s.Id,
                }),
            });
            Assert.Equal($@"query TestQuery ($a0: [Int]) {{
Movies: moviesByIds(ids: $a0) {{
Id: id
}}
}}", query.Query, ignoreLineEndingDifferences: true);

            Assert.Equal(@"{""a0"":[1,2,5]}", JsonConvert.SerializeObject(query.Variables), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void TestArrayArgExprInited()
        {
            var client = new TestClient();
            var query = client.MakeQuery(q => new
            {
                Movies = q.MoviesByIds(new List<int?> { 1, 2, 5 }, s => new
                {
                    s.Id,
                }),
            });
            Assert.Equal($@"query TestQuery ($a0: [Int]) {{
Movies: moviesByIds(ids: $a0) {{
Id: id
}}
}}", query.Query, ignoreLineEndingDifferences: true);

            Assert.Equal(@"{""a0"":[1,2,5]}", JsonConvert.SerializeObject(query.Variables), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void TestComplexValueArg()
        {
            var client = new TestClient();
            var query = client.MakeQuery(q => new
            {
                Producers = q.Producers(new FilterBy { Field = "lastName", Value = "Lucas" }, s => new
                {
                    s.Id,
                    s.LastName
                }),
            });
            Assert.Equal($@"query TestQuery  {{
Producers: producers(filter: {{ field: ""lastName"", value: ""Lucas"" }}) {{
Id: id
LastName: lastName
}}
}}", query.Query, ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void TestErrorOnInvalidPropertySelection()
        {
            Assert.Throws<ArgumentException>(() => {
                var client = new TestClient();
                var query = client.MakeQuery(q => new
                {
                    Movie = q.Movies(s => new
                    {
                        // we generate gql we don't select the .Value the value will be serialised in the object we're creating
                        s.Rating.Value,
                    }),
                });
            });
        }
        [Fact]
        public void TestErrorOnInvalidPropertySelection2()
        {
            Assert.Throws<ArgumentException>(() => {
                var client = new TestClient();
                var query = client.MakeQuery(q => new
                {
                    Movie = q.Movies(s => new
                    {
                        // we generate gql we don't select the .Value the value will be serialised in the object we're creating
                        s.Director().Died
                    }),
                });
            });
        }
    }

    public class MovieResult
    {
        public int Id { get; set; }
    }

    public class MyResult
    {
        public List<MovieResult> Movies { get; set; }
    }
}
