using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestResource : ZendeskResource<Request>, IRequestResource
    {
        private const string ResourceUri = "/api/v2/requests";

        public RequestResource(IRestClient client)
        {
            Client = client;
        }

        public async Task<IResponse<Request>> GetAsync(long id)
        {
            return await GetAsync<RequestResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public async Task<IResponse<Request>> GetAsync(IEnumerable<TicketStatus> requestedStatuses)
        {
            string query = $"status={string.Join(",", requestedStatuses).ToLower()}";
            return await GetAsync<RequestResponse>(ResourceUri, query).ConfigureAwait(false);
        }

        public async Task<IResponse<Request>> PutAsync(RequestRequest request)
        {
            ValidateRequest(request);
            return await PutAsync<RequestRequest, RequestResponse>(request, $"{ResourceUri}/{request.Item.Id}").ConfigureAwait(false);
        }

        public async Task<IResponse<Request>> PostAsync(RequestRequest request)
        {
            return await PostAsync<RequestRequest, RequestResponse>(request, ResourceUri).ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            ValidateRequest(id);
            await DeleteAsync($"{ResourceUri}/{id}").ConfigureAwait(false);
        }
    }
}
