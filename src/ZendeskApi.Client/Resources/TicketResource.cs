using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketResource : ZendeskResource<Ticket>, ITicketResource
    {
        private const string ResourceUri = "/api/v2/tickets";

        public TicketResource(IRestClient client)
        {
            Client = client;
        }

        public async Task DeleteAsync(long id)
        {
            ValidateRequest(id);
            await DeleteAsync($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public async Task<IResponse<Ticket>> GetAsync(long id)
        {
            return await GetAsync<TicketResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public async Task<IListResponse<Ticket>> GetAllAsync(List<long> ids)
        {
            return await GetAllAsync<TicketListResponse>($"{ResourceUri}/show_many", ids).ConfigureAwait(false);
        }

        public async Task<IResponse<Ticket>> PutAsync(TicketRequest request)
        {
            ValidateRequest(request);
            return await PutAsync<TicketRequest, TicketResponse>(request, $"{ResourceUri}/{request.Item.Id}").ConfigureAwait(false);
        }

        public async Task<IResponse<Ticket>> PostAsync(TicketRequest request)
        {
            return await PostAsync<TicketRequest, TicketResponse>(request, ResourceUri).ConfigureAwait(false);
        }
    }
}