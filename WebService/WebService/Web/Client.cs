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
        public static async Task RequestPostAsync(string uri, string jsonRequestContent) // TODO: сделать возврат ответа
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            
            request.RequestUri = new Uri(uri);
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", "application/json");
            
            HttpContent content = new StringContent(jsonRequestContent, Encoding.UTF8, "application/json");
            request.Content = content;

            HttpResponseMessage response = await client.SendAsync(request); // отправка запроса и получение ответа

            var jsonType = new { success = false, result = "" };
            var jsonResponseContent = await response.Content.ReadAsStringAsync();

            try
            {
                var responseContent = JsonConvert.DeserializeAnonymousType(jsonResponseContent, jsonType);
                if (responseContent == null)
                    throw new Exception("Client.responseContext == null");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine($"\nЗапрос по адресу {uri} прошёл успешно!\n" +
                                      "Отправленные данные:");
                    Console.WriteLine(jsonRequestContent);
                    Console.WriteLine("Полученные данные");
                    Console.WriteLine(responseContent.result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nЗапрос по адресу {uri} прошёл неудачно! Код ошибки: {response.StatusCode}\n" +
                                  "Отправленные данные:");
                Console.WriteLine(jsonRequestContent);
                Console.WriteLine("Полученные данные");
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine(e);
            }
        }
    }
}
