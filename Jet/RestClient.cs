using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jet.Messages;
using Newtonsoft.Json;

namespace Jet
{
    public static class RestClient
    {
        private static HttpClient _client;

        /// <summary>
        /// Build http client if null
        /// </summary>
        private static void Connect()
        {
            if (_client != null)
            {
                return;
            }

            _client = new HttpClient
            {
                BaseAddress = new Uri("https://merchant-api.jet.com/api/"),
            };
        }

        /// <summary>
        /// Posts request to IParcel with specified endpoint
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static async Task<TResult> Get<TResult>(string queryString, string endPoint)
        {
            Connect();

            var response = await _client.GetAsync($"{endPoint}?{queryString}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<TResult>(responseContent);
            return result;
        }

        /// <summary>
        /// Posts request to IParcel with specified endpoint and checks only that it successed
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static async Task<bool> Post<TRequest>(TRequest request, string endPoint) where TRequest : BaseRequest
        {
            Connect();

            //Log("Request", request); // log request for debugging

            var serializedContent = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            var requestContent = new StringContent(serializedContent, request.Encoding, request.MediaContentType);

            var response = await _client.PostAsync(endPoint, requestContent).ConfigureAwait(false);
            
            response.EnsureSuccessStatusCode();
            return true;
        }

        /// <summary>
        /// Posts request to IParcel with specified endpoint
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="request"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static async Task<TResult> Post<TRequest, TResult>(TRequest request, string endPoint) where TRequest : BaseRequest
        {
            Connect();

            //Log("Request", request); // log request for debugging

            var serializedContent = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var requestContent = new StringContent(serializedContent, request.Encoding, request.MediaContentType);

            var response = await _client.PostAsync(endPoint, requestContent).ConfigureAwait(false);
            
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Blahh");
            }

            var result = JsonConvert.DeserializeObject<TResult>(responseContent);

            //Log("Result", result); // log result for debugging

            return result;
        }
    }
}
