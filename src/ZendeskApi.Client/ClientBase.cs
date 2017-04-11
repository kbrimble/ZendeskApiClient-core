﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using HttpChannel = ZendeskApi.Client.Http.HttpChannel;
using HttpRequest = ZendeskApi.Client.Http.HttpRequest;
using IHttpRequest = ZendeskApi.Client.Http.IHttpRequest;
using IHttpResponse = ZendeskApi.Client.Http.IHttpResponse;
using ILogAdapter = ZendeskApi.Client.Logging.ILogAdapter;
using IRestClient = ZendeskApi.Client.Http.IRestClient;
using ISerializer = ZendeskApi.Client.Serialization.ISerializer;


namespace ZendeskApi.Client
{
    public abstract class ClientBase : IRestClient
    {
        private readonly Uri _baseUri;

        private readonly ZendeskDefaultConfiguration _configuration;

        private readonly IHttpChannel _http;

        private readonly ISerializer _serializer;

        private string _clientName;
        protected string ClientName
        {
            private get { return _clientName ?? (_clientName = GetType().Name); }
            set { _clientName = value; }
        }


        protected ClientBase(
            Uri baseUri, 
            ZendeskDefaultConfiguration configuration, 
            ISerializer serializer = null, 
            IHttpChannel httpChannel = null, 
            ILogAdapter loggerAdapter = null
            )
        {
            if (baseUri == null)
                throw new ArgumentNullException("baseUri");
            var logger = loggerAdapter ?? new Logging.SystemDiagnosticsAdapter();
            _baseUri = baseUri;
            _configuration = configuration;
            _http = httpChannel ?? new HttpChannel();
            _serializer = serializer ?? new Serialization.ZendeskJsonSerializer();
            logger.Debug(string.Format("Created Zendesk client. BaseUri: {0}, Serializer: {1}, HttpChannel: {2}, Logger: {3}",
                _baseUri, _serializer.GetType().Name, _http.GetType().Name, logger.GetType().Name));
    
        }

        public Uri BuildUri(string handler, string query = "")
        {
            return new UriBuilder(_baseUri)
            {
                Path = handler,
                Query = query
            }.Uri;
        }

        public async Task<T> GetAsync<T>(Uri requestUri, string resource, string operation)
        {
            var request = BuildRequest(requestUri);
            var response = await _http.GetAsync(request).ConfigureAwait(false);
            ValidateResponse(response);
            return DeserializeContent<T>(response);
        }

        public async Task<T> PostFileAsync<T>(Uri requestUri, IHttpPostedFile file, string resource, string operation)
        {
            var response = await _http.PostAsync(requestUri, file);

            ValidateResponse(response);

            return DeserializeContent<T>(response);
        }

        public async Task<T> PostAsync<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            var request = BuildRequest(requestUri, item, contentType);
            var response = await _http.PostAsync(request).ConfigureAwait(false);
            ValidateResponse(response);
            return DeserializeContent<T>(response);
        }

        public async Task<T> PutAsync<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            var request = BuildRequest(requestUri, item, contentType);
            var response = await _http.PutAsync(request).ConfigureAwait(false);
            ValidateResponse(response);
            return DeserializeContent<T>(response);
        }

        public async Task<T> DeleteAsync<T>(Uri requestUri, object item, string contentType, string resource, string operation)
        {
            var request = BuildRequest(requestUri);
            var response = await _http.DeleteAsync(request).ConfigureAwait(false);
            ValidateResponse(response);
            return DeserializeContent<T>(response);
        }

        private IHttpRequest BuildRequest(Uri requestUri)
        {
            return new HttpRequest(requestUri, _configuration.Headers, null, null, _configuration.RequestTimeout);
        }

        private IHttpRequest BuildRequest(Uri requestUri, string contentType)
        {
            return BuildRequest(requestUri, null, contentType);
        }

        private IHttpRequest BuildRequest(Uri requestUri, object item, string contentType)
        {
            var content = (string) null;
            if (item != null)
                content = _serializer.Serialize(item);
            return new HttpRequest(requestUri, _configuration.Headers, content, contentType,
                _configuration.RequestTimeout);
        }

        private static void ValidateResponse(IHttpResponse response)
        {
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.Content);
        }

        private T DeserializeContent<T>(IHttpResponse response)
        {
            return _serializer.Deserialize<T>(response.Content);
        }
    }
}
