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
/// Generated on 25/3/20 1:18:08 pm from ../../../../../xysense/xynger/src/Web/XyAdmin/src/app/generated/admin.graphql
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

    public class GraphQLClient : BaseGraphQLClient
    {
        private Uri apiUrl;
        private readonly HttpClient client;

        public GraphQLClient()
        {
            this.apiUrl = new Uri("");
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public GraphQLClient(HttpClient client)
            : this(client.BaseAddress, client)
        {
        }

        public GraphQLClient(Uri apiUrl, HttpClient client)
        {
            this.apiUrl = apiUrl;
            this.client = client;
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<GqlResult<TQuery>> ProcessResult<TQuery>(string gql)
        {
            // gql is a GraphQL doc e.g. { query MyQuery { stuff { id name } } }
            // you can replace the following with what ever HTTP library you use
            // don't forget to implement your authentication if required
            var req = new HttpRequestMessage {
                RequestUri = this.apiUrl,
                Method = HttpMethod.Post,
            };
            var queryReq = new
            {
                Query = gql.ToString()
            };
            req.Content = new StringContent(JsonConvert.SerializeObject(queryReq), Encoding.UTF8, "application/json");
            // you will need to implement any auth
            // req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await client.SendAsync(req);
            var strResult = await res.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<GqlResult<TQuery>>(strResult);
            return data;
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
