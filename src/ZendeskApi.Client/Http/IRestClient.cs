using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ZendeskApi.Client.Logging;
using ZendeskApi.Client.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Client.Http
{
    public interface IRestClient
    {
        Uri BuildUri(string handler, string query = "");

        Task<T> GetAsync<T>(
            Uri requestUri,
            [CallerFilePath] string resource = "",
            [CallerMemberName] string operation = ""
            );

        Task<T> PostAsync<T>(
            Uri requestUri,
            object item = null,
            string contentType = "application/json",
            [CallerFilePath] string resource = "",
            [CallerMemberName] string operation = ""
            );

        Task<T> PutAsync<T>(
            Uri requestUri,
            object item = null,
            string contentType = "application/json",
            [CallerFilePath] string resource = "",
            [CallerMemberName] string operation = ""
            );

        Task<T> DeleteAsync<T>(
            Uri requestUri,
            object item = null,
            string contentType = "application/json",
            [CallerFilePath] string resource = "",
            [CallerMemberName] string operation = ""
            );

        Task<T> PostFileAsync<T>(
            Uri requestUri,
            IHttpPostedFile file,
            [CallerFilePath] string resource = "",
            [CallerMemberName] string operation = ""
            );
    }
}