using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestCommentResource : ZendeskResource<TicketComment>, IRequestCommentResource
    {
        private const string ResourceUri = "/api/v2/requests/{0}/comments";

        public RequestCommentResource(IRestClient client)
        {
            Client = client;
        }

        public async Task<IResponse<TicketComment>> GetAsync(long id, long parentId)
        {
            string url = $"{string.Format(ResourceUri, parentId)}/{id}";
            return await GetAsync<TicketCommentResponse>(url).ConfigureAwait(false);
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            string url = string.Format(ResourceUri, parentId);
            return await GetAllAsync<TicketCommentListResponse>(url).ConfigureAwait(false);
        }
    }
}
