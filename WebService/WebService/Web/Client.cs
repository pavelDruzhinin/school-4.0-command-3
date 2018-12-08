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
        /// <summary>
        /// GET-запрос, если нет данных, POST - если есть
        /// </summary>
        /// <param name="uri">Адрес, на который отправляется запрос</param>
        /// <param name="jsonRequestContent">Данные в формате JSON для POST запроса</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> RequestAsync(string uri, string jsonRequestContent = "")
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            
            request.RequestUri = new Uri(uri);
            // GET-запрос, если нет данных
            if (jsonRequestContent == "")
            {
                request.Method = HttpMethod.Get;
                return await client.GetAsync(uri);
            }
            // POST-запрос, если есть данные
            else
            {
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");               
                HttpContent content = new StringContent(jsonRequestContent, Encoding.UTF8, "application/json");
                request.Content = content;              
                return await client.SendAsync(request); // отправка запроса и получение ответа
            }

        }
    }
}
