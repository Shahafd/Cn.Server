using CN.Common.Configs;
using CN.Common.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Services
{
    public class HttpClientSender : IHttpClient
    {
        public HttpClient client { get; set; }

        public object GetRequest(string route)
        {
            using (client = new HttpClient())
            {
                InitHttpClient();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + route).Result;
                object result = response.Content.ReadAsAsync(typeof(object)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return result;
                }
                else
                {
                    return response.StatusCode;
                }
            }

        }

        public object PostRequest(string route, object obj)
        {
            using (client = new HttpClient())
            {
                InitHttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(client.BaseAddress + route, obj).Result;
                object result = response.Content.ReadAsAsync(typeof(object)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return result;
                }
                else
                {
                    return response.StatusCode;
                }
            }
        }
        public void InitHttpClient()
        {
            client.BaseAddress = new Uri(MainConfigs.Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
