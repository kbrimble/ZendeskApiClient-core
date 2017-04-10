using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace ZendeskApi.Client.Resources
    {
        public abstract class ZendeskResource<T> where T : IZendeskEntity
        {
            protected IRestClient Client;

            private string _resourceName;
            protected string ResourceName
            {
                private get { return _resourceName ?? (_resourceName = GetType().Name); }
                set { _resourceName = value; }
            }

            protected async Task<IResponse<T>> GetAsync<TResponse>(string url) where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected async Task<IResponse<T>> GetAsync<TResponse>(string url, string query) where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url, query);
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected async Task<TResponse> GetAllAsync<TResponse>(string url, IEnumerable<long> ids) where TResponse : IListResponse<T>
            {
                var requestUri = Client.BuildUri(url, $"ids={ZendeskFormatter.ToCsv(ids)}");
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected async Task<TResponse> GetAllAsync<TResponse>(string url) where TResponse : IListResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return await Client.GetAsync<TResponse>(requestUri, resource: ResourceName).ConfigureAwait(false);
            }

            protected async Task<IResponse<T>> PutAsync<TRequest, TResponse>(TRequest request, string url)
                where TRequest : IRequest<T>
                where TResponse : IResponse<T>
            {
                if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                    throw new ArgumentException("Item must exist in Zendesk");

                var requestUri = Client.BuildUri(url);
                return await Client.PutAsync<TResponse>(requestUri, request, resource: ResourceName).ConfigureAwait(false);
            }

            protected async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string url)
                where TRequest : IRequest<T>
                where TResponse : IResponse<T>
            {
                var requestUri = Client.BuildUri(url);
                return await Client.PostAsync<TResponse>(requestUri, request, resource: ResourceName).ConfigureAwait(false);
            }

            public async Task DeleteAsync(string url)
            {
                var requestUri = Client.BuildUri(url);
                await Client.DeleteAsync<object>(requestUri, null, resource: ResourceName).ConfigureAwait(false);
            }

            protected void ValidateRequest<TReq>(IRequest<TReq> request) where TReq : IZendeskEntity
            {
                if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                    throw new ArgumentException("Item must exist in Zendesk");
            }

            public void ValidateRequest(long id)
            {
                if (id <= 0)
                    throw new ArgumentException("Item must exist in Zendesk");
            }

        }
    }
}
