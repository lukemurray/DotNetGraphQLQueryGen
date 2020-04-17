using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DotNetGqlClient;
using Newtonsoft.Json;

/// <summary>
/// Generated interfaces for making GraphQL API calls with a typed interface.
///
/// Generated on 17/4/20 9:30:56 pm from ../tests/DotNetGraphQLQueryGen.Tests/schema.graphql
/// </summary>

namespace Generated
{
    public class GqlError
    {
        public string Message { get; set; }
    }
    public class GqlResult<TQuery>
    {
        public List<GqlError> Errors { get; set; }
        public TQuery Data { get; set; }
    }

    public class TestHttpClient : BaseGraphQLClient
    {
        public TestHttpClient()
        {
            this.typeMappings = new Dictionary<string, string> {
                    { "string" , "String" },
                    { "String" , "String" },
                    { "int" , "Int!" },
                    { "Int32" , "Int!" },
                    { "double" , "Float!" },
                    { "bool" , "Boolean!" },
                    { "DateTime" , "Date" },
            };
        }

        protected virtual Task<GqlResult<TQuery>> ProcessResult<TQuery>(QueryRequest gql)
        {
            return null;
        }

        public async Task<GqlResult<TQuery>> QueryAsync<TQuery>(Expression<Func<RootQuery, TQuery>> query)
        {
            var gql = base.MakeQuery<RootQuery, TQuery>(query);
            return await ProcessResult<TQuery>(gql);
        }

        public async Task<GqlResult<TQuery>> MutateAsync<TQuery>(Expression<Func<Mutation, TQuery>> query)
        {
            var gql = base.MakeQuery<Mutation, TQuery>(query, true);
            return await ProcessResult<TQuery>(gql);
        }
    }

}
