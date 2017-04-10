using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
{
    public interface IHttpChannel
    {
        Task<IHttpResponse> GetAsync(IHttpRequest request, string clientName, string resourceName, string operation);
        Task<IHttpResponse> PostAsync(IHttpRequest request, string clientName, string resourceName, string operation);
        Task<IHttpResponse> PutAsync(IHttpRequest request, string clientName, string resourceName, string operation);
        Task<IHttpResponse> DeleteAsync(IHttpRequest request, string clientName, string resourceName, string operation);
        Task<IHttpResponse> PostAsync(IHttpRequest req, IHttpPostedFile file, string clientName, string resourceName, string operation);
    }
}