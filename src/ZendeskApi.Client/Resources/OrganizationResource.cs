using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationResource : ZendeskResource<Organization>, IOrganizationResource
    {
        private const string ResourceUri = "/api/v2/organizations";

        public OrganizationResource(IRestClient client)
        {
            Client = client;
        }

        public async Task<IResponse<Organization>> GetAsync(long id)
        {
            string url = $"{ResourceUri}/{id}";
            return await GetAsync<OrganizationResponse>(url).ConfigureAwait(false);
        }
        public async Task<IResponse<Organization>> PutAsync(OrganizationRequest request)
        {
            ValidateRequest(request);

            string url = $"{ResourceUri}/{request.Item.Id}";
            return await PutAsync<OrganizationRequest, OrganizationResponse>(request, url).ConfigureAwait(false);
        }

        public async Task<IResponse<Organization>> PostAsync(OrganizationRequest request)
        {
            return await PostAsync<OrganizationRequest, OrganizationResponse>(request, ResourceUri).ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            ValidateRequest(id);
            await DeleteAsync($"{ResourceUri}/{id}").ConfigureAwait(false);
        }
    }
}
