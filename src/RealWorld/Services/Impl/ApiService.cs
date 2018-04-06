using RealWorld.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RealWorld.Services.Impl
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;
        private readonly IServiceProvider _serviceProvider;

        public ApiService(HttpClient client, IServiceProvider serviceProvider)
        {
            this._client = client;
            this._serviceProvider = serviceProvider;
        }

        private void InitializeAuthToken(bool autheticate)
        {
            var userService = (IUserService)_serviceProvider.GetService(typeof(IUserService));
            //var token = RegisteredFunction.Invoke<string>("getSessionToken");
            //Console.WriteLine($"Token = {(userService.CurrentUser != null ? userService.CurrentUser.token : null)}");

            this._client.DefaultRequestHeaders.Authorization = autheticate && userService.CurrentUser != null ?
                new AuthenticationHeaderValue("Token", userService.CurrentUser.token) : null;

            //this._client.DefaultRequestHeaders.Authorization = autheticate && !string.IsNullOrWhiteSpace(token) ? 
            //    new AuthenticationHeaderValue("Token", token) : null;            
        }

        private async Task<T> HandleApiRespose<T>(HttpResponseMessage responseMessage)
        {
            var responseJson = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseObject = JsonUtil.Deserialize<T>(responseJson);

                //if (responseObject is SingleUser)
                //{
                //    var token = ((SingleUser)(object)responseObject).user.token;
                //    RegisteredFunction.Invoke<string>("setSessionToken", token);
                //}

                return responseObject;
            }

            Console.WriteLine($"ApiServiceException: (status:{responseMessage.StatusCode}) {responseJson}");

            if (!string.IsNullOrWhiteSpace(responseJson))
                throw new ApiServiceException(JsonUtil.Deserialize<Error>(responseJson));

            throw new ApiServiceException();
        }

        private async Task HandleApiRespose(HttpResponseMessage responseMessage)
        {
            var responseJson = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
                return;

            Console.WriteLine($"ApiServiceException:{responseJson}");

            if (!string.IsNullOrWhiteSpace(responseJson))
                throw new ApiServiceException(JsonUtil.Deserialize<Error>(responseJson));

            throw new ApiServiceException();
        }

        private string BuildUri(string apiUrl, IDictionary<string, string> queryParams = null)
        {
            var uri = $"{Consts.CONDUIT_API_URL}{apiUrl}";

            if (queryParams != null)
                queryParams = queryParams.Where(_ => _.Value != null).ToDictionary(_ => _.Key, _ => _.Value);

            if (queryParams != null && queryParams.Any())
                uri = QueryHelpers.AddQueryString(uri, queryParams);

            return uri;
        }

        public async Task<T> GetAsync<T>(string apiUrl, bool autheticate = false, IDictionary<string, string> queryParams = null)
        {
            InitializeAuthToken(autheticate);

            return await HandleApiRespose<T>(
                await _client.GetAsync(BuildUri(apiUrl, queryParams)));
        }

        public async Task<T> PostAsync<T>(string apiUrl, object content = null, bool autheticate = false, IDictionary<string, string> queryParams = null)
        {
            InitializeAuthToken(autheticate);

            var httpContent = content != null ? new StringContent(JsonUtil.Serialize(content)) : null;
            if (httpContent != null)
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await HandleApiRespose<T>(
                await _client.PostAsync(BuildUri(apiUrl, queryParams), httpContent));
        }

        public async Task<T> PutAsync<T>(string apiUrl, object content = null, bool autheticate = false, IDictionary<string, string> queryParams = null)
        {
            InitializeAuthToken(autheticate);

            var httpContent = content != null ? new StringContent(JsonUtil.Serialize(content)) : null;
            if (httpContent != null)
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await HandleApiRespose<T>(
                await _client.PutAsync(BuildUri(apiUrl, queryParams), httpContent));
        }

        public async Task<T> DeleteAsync<T>(string apiUrl, bool autheticate = false, IDictionary<string, string> queryParams = null)
        {
            InitializeAuthToken(autheticate);

            return await HandleApiRespose<T>(
                await _client.DeleteAsync(BuildUri(apiUrl, queryParams)));
        }

        public async Task DeleteAsync(string apiUrl, bool autheticate = false, IDictionary<string, string> queryParams = null)
        {
            InitializeAuthToken(autheticate);

            await HandleApiRespose(
                await _client.DeleteAsync(BuildUri(apiUrl, queryParams)));
        }
    }
}
