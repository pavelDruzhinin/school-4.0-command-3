using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebService
{
    public class Client
    {
        public static async Task<HttpResponseMessage> RequestPostAsync(string uri, string jsonRequestContent)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            
            request.RequestUri = new Uri(uri);
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", "application/json");
            
            HttpContent content = new StringContent(jsonRequestContent, Encoding.UTF8, "application/json");
            request.Content = content;

            return await client.SendAsync(request); // отправка запроса и получение ответа
        }
    }
}
