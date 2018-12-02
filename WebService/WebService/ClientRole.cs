using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebService
{
    public class ClientRole
    {
        public static async Task RequestAsync(string uri, IList<int> idList)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();

            request.RequestUri = new Uri(uri);
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", "application/json");
            string requestContentJson = JsonConvert.SerializeObject(idList);
            HttpContent content = new StringContent(requestContentJson, Encoding.UTF8, "application/json");
            request.Content = content;

            HttpResponseMessage response = await client.SendAsync(request); // отправка запроса и получение ответа

            var jsonType = new {success = false, result = ""};
            var responseContentJson = await response.Content.ReadAsStringAsync();

            var responseContent = JsonConvert.DeserializeAnonymousType(responseContentJson, jsonType);
            
            if (response.StatusCode == HttpStatusCode.OK && responseContent.success)
            {
                Console.WriteLine($"\nЗапрос по адресу {uri} прошёл успешно!\n" +
                                  "Отправленные данные:");
                Console.WriteLine(requestContentJson);
                Console.WriteLine("Полученные данные");
            }
            else
            {
                Console.WriteLine($"\nЗапрос по адресу {uri} завершился ошибкой! Код ошибки: {response.StatusCode}\n" +
                                  "Отправленные данные:");
                Console.WriteLine(requestContentJson);
                Console.WriteLine("Полученные данные:");
            }
            Console.WriteLine(responseContent.result);
        }
    }
}