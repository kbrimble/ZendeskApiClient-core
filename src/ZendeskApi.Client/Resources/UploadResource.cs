using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UploadResource : ZendeskResource<Upload>, IUploadResource
    {
        private const string ResourceUri = "/api/v2/uploads";

        public UploadResource(IRestClient client)
        {
            Client = client;
        }

        public void Delete(string token)
        {
            Delete($"{ResourceUri}/{token}");
        }

        public async Task<IResponse<Upload>> GetAsync(long id)
        {
            return await GetAsync<UploadResponse>($"{ResourceUri}/{id}").ConfigureAwait(false); ;
        }

        public async Task<IResponse<Upload>> PostAsync(UploadRequest request)
        {
            var requestUri = Client.BuildUri(ResourceUri, $"filename={request.Item.FileName}{request.Token ?? ""}");
            return await Client.PostFileAsync<UploadResponse>(requestUri, request.Item);
        }
    }
}
