using System;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
{
    public interface IHttpChannel
    {
        Task<IHttpResponse> GetAsync(IHttpRequest request);
        Task<IHttpResponse> PostAsync(IHttpRequest request);
        Task<IHttpResponse> PutAsync(IHttpRequest request);
        Task<IHttpResponse> DeleteAsync(IHttpRequest request);
        Task<IHttpResponse> PostAsync(Uri requestUri, IHttpPostedFile file);
    }
}