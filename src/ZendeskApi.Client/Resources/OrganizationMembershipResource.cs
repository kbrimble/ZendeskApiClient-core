using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationMembershipResource : ZendeskResource<OrganizationMembership>, IOrganizationMembershipResource
    {
        private const string UsersUrl = "/api/v2/users/{0}/organization_memberships";
        private const string OrganisationsUrl = "/api/v2/organizations/{0}/organization_memberships";

        public OrganizationMembershipResource(IRestClient client)
        {
            Client = client;
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByOrganizationAsync(long organizationId)
        {
            string url = string.Format(OrganisationsUrl, organizationId);
            return await GetAllAsync<OrganizationMembershipListResponse>(url).ConfigureAwait(false); ;
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByUserAsync(long userId)
        {
            string url = string.Format(UsersUrl, userId);
            return await GetAllAsync<OrganizationMembershipListResponse>(url).ConfigureAwait(false);
        }

        public async Task<IResponse<OrganizationMembership>> PostAsync(OrganizationMembershipRequest request)
        {
            string url = string.Format(UsersUrl, request.Item.UserId);
            return await PostAsync<OrganizationMembershipRequest, OrganizationMembershipResponse>(request, url).ConfigureAwait(false);
        }
    }
}
