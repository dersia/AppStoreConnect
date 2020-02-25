using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppStoreConnect.Endpoints.Base
{
    public abstract class EndpointBase
    {
        private static HttpClient? _httpClient;

        public EndpointBase(string bearerToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
        }

        protected HttpClient Client
        { 
            get 
            { 
                if(_httpClient is null)
                {
                    throw new ArgumentNullException(nameof(_httpClient));
                }
                return _httpClient; 
            } 
        }
    }
}
