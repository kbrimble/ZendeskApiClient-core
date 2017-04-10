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
    public class UserResource : ZendeskResource<User>, IUserResource
    {
        private const string ResourceUri = "/api/v2/users/";

        public UserResource(IRestClient client)
        {
            Client = client;
        }

        public async Task<IResponse<User>> GetAsync(long id)
        {
            return await GetAsync<UserResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public async Task<IListResponse<User>> GetAllAsync(List<long> ids)
        {
            return await GetAllAsync<UserListResponse>($"{ResourceUri}/show_many", ids).ConfigureAwait(false);
        }

        public async Task<IResponse<User>> PostAsync(UserRequest request)
        {
            return await PostAsync<UserRequest, UserResponse>(request, ResourceUri).ConfigureAwait(false);
        }

        public async Task<IResponse<User>> PutAsync(UserRequest request)
        {
            ValidateRequest(request);
            return await PutAsync<UserRequest, UserResponse>(request, 
                $"{ResourceUri}/{request.Item.Id}").ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            await DeleteAsync($"{ResourceUri}/{id}");
        }
    }
}
