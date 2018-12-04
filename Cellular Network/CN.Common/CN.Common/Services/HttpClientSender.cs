using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Services
{
    public class HttpClientSender : IHttpClient
    {
        public ILogger logger { get; set; }
        public HttpClientSender(ILogger logger)
        {
            this.logger = logger;
        }
        public HttpClient client { get; set; }

        public Tuple<object, HttpStatusCode> GetRequest(string route)
        {
            using (client = new HttpClient())
            {
                InitHttpClient();
                try
                {
                    HttpResponseMessage response = client.GetAsync(client.BaseAddress + route).Result;
                    object result = response.Content.ReadAsAsync(typeof(object)).Result;
                    return new Tuple<object, HttpStatusCode>(result, response.StatusCode);
                }
                catch (AggregateException e)
                {
                    logger.Print(e.Message);
                    //Write to error table
                    return new Tuple<object, HttpStatusCode>(null, HttpStatusCode.NotFound);
                }
            }
        }

        public Tuple<object, HttpStatusCode> PostRequest(string route, object obj = null)
        {
            using (client = new HttpClient())
            {
                InitHttpClient();
                try
                {
                    HttpResponseMessage response = client.PostAsJsonAsync(client.BaseAddress + route, obj).Result;
                    object result = response.Content.ReadAsAsync(typeof(object)).Result;
                    return new Tuple<object, HttpStatusCode>(result, response.StatusCode);
                }
                catch (AggregateException e)
                {
                    logger.Print(e.Message);
                    //Write to error table
                    return new Tuple<object, HttpStatusCode>(null, HttpStatusCode.NotFound);
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
