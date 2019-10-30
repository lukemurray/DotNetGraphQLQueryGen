using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNetGqlClient;
using Generated;
using Xunit;
using Xunit.Abstractions;

namespace CoreData.Model.Tests
{
    public class TestClient : BaseGraphQLClient
    {
        internal string MakeQuery<TReturn>(Expression<Func<RootQuery, TReturn>> p, bool mutation = false)
        {
            return base.MakeQuery(p, mutation);
        }
        internal string MakeMutation<TReturn>(Expression<Func<Mutation, TReturn>> p, bool mutation = false)
        {
            return base.MakeQuery(p, mutation);
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
            Assert.Equal($@"query BaseGraphQLClient {{
Movies: movies {{
Id: id
}}
}}".Replace("\r\n", "\n"), query);
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
            Assert.Equal($@"query BaseGraphQLClient {{
Actors: actors {{
Id: id
DirectorOf: directorOf {{
Id: id
Name: name
Genre: genre
Released: released
DirectorId: directorId
Rating: rating
}}
}}
}}".Replace("\r\n", "\n"), query);
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
            Assert.Equal($@"query BaseGraphQLClient {{
Movies: movies {{
Id: id
}}
}}".Replace("\r\n", "\n"), query);
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
            Assert.Equal($@"mutation BaseGraphQLClient {{
Movie: addMovie(name: ""movie"", rating: 5.5, released: ""2019-10-30T17:55:23.0000000"") {{
Id: id
}}
}}".Replace("\r\n", "\n"), query);
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
