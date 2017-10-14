using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GranularMonitorSystem.Services.RequestProvider;
using GranularMonitorSystem.Service.Exceptions;
using System.Text;

namespace GranularMonitorSystem.Services.API.Requests
{
    public class Requests: IRequests
    {
        private readonly JsonSerializerSettings _serializerSettings;

            public Requests()
            {
                _serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    NullValueHandling = NullValueHandling.Ignore
                };
                _serializerSettings.Converters.Add(new StringEnumConverter());
            }

            public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
            {
                HttpClient httpClient = CreateHttpClient(token:token);
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                await HandleResponse(response);
                string serialized = await response.Content.ReadAsStringAsync();

                TResult result = await Task.Run(() => 
                                            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

                return result;
            }

            public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "")
            {
            HttpClient httpClient = CreateHttpClient(token:token);

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                await HandleResponse(response);
                string serialized = await response.Content.ReadAsStringAsync();

                TResult result = await Task.Run(() =>
                                            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

                return result;
            }

            public async Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret)
            {
                HttpClient httpClient = CreateHttpClient(string.Empty);

                if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
                {
                    AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
                }

                var content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                await HandleResponse(response);
                string serialized = await response.Content.ReadAsStringAsync();

                TResult result = await Task.Run(() =>
                                            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

                return result;
            }

            public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "")
            {
                HttpClient httpClient = CreateHttpClient(token);

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PutAsync(uri, content);

                await HandleResponse(response);
                string serialized = await response.Content.ReadAsStringAsync();

                TResult result = await Task.Run(() =>
                                            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

                return result;
            }

            public async Task DeleteAsync(string uri, string token = "")
            {
                HttpClient httpClient = CreateHttpClient(token);
                await httpClient.DeleteAsync(uri);
            }

            private HttpClient CreateHttpClient(string username = "", string password = "", string token = "")
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(token))
                {
                    var byteArray = Encoding.UTF8.GetBytes( token + ":"+"unused");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if(!string.IsNullOrEmpty(username) & !string.IsNullOrEmpty(password))
                {
                    var byteArray = Encoding.UTF8.GetBytes( username + ":"+ password);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }

                return httpClient;
            }

            private void AddHeaderParameter(HttpClient httpClient, string parameter)
            {
                if (httpClient == null)
                    return;

                if (string.IsNullOrEmpty(parameter))
                    return;

                httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
            }

            private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
            {
                if (httpClient == null)
                    return;

                if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                    return;
            }

            private async Task HandleResponse(HttpResponseMessage response)
            {
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Forbidden || 
                    response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        
                        //throw new Exception();//ServiceAuthenticationException(content);
                    }

                    throw new HttpRequestExceptionEx(response.StatusCode, content);
                }
            }

        public async Task<TResult> LoginAsync<TResult>(string uri,string username = "", string password = "")
        {
            HttpClient httpClient = CreateHttpClient(username,password);
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string token = response.RequestMessage.Headers.Authorization.Parameter;

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() => 
                                                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;  
        }

        public async Task<string> LoginAsync(string uri, string username = "", string password = "")
        {
            HttpClient httpClient = CreateHttpClient(username, password);
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string token = response.RequestMessage.Headers.Authorization.Parameter;

            string serialized = await response.Content.ReadAsStringAsync();

            //TResult result = await Task.Run(() =>
            //                                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return token;
        }
    }
}
