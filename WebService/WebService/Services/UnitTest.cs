using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebService.Services
{
    public class UnitTest : IService
    {
        public string Uri { get; }
        public string ConnectionUri { get; }

        public UnitTest()
        {
            Uri = ""; // Адрес самого сервиса
            ConnectionUri = "http://localhost:50321/"; // Адрес подключения
        }      

        public async void Run()
        {
            // Для GET-запроса (когда данные передавать не нужно)
            await SendGetRequest(ConnectionUri + "product/1"); 


            // Для POST-запроса (когда есть данные для передачи)
            // Сначала нужно сделать DTO, которую будем передавать
            var dto = new
            {
                // Имена должны совпадать с именами объекта DTO, можно инициализировать не все поля, например
                Name = "My first product",
                ShortDescription = "This is a good product"
            };
            await SendPostRequest(ConnectionUri + "product/add", dto);
        }

        private async Task SendGetRequest(string uri)
        {
            try
            {
                var response = await Client.RequestAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"\nUnitTest.SendGetRequest(): GET-запрос по адресу {uri} прошёл успешно!");
                    //Console.WriteLine("Полученные данные:");
                    //string responseContent = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"\nUnitTest.SendGetRequest(): GET-запрос по адресу {uri} прошёл неудачно!");
                    Console.WriteLine($"Статус ошибки: {response.StatusCode}");
                    Console.WriteLine($"Причина: {response.ReasonPhrase}");
                }       
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private async Task SendPostRequest(string uri, object data)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(data); // формиование JSON
                var response = await Client.RequestAsync(uri, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"\nUnitTest.SendPostRequest(): POST-запрос по адресу {uri} прошёл успешно!");
                    Console.WriteLine($"Переданные данные: {jsonContent}");

                    //Console.WriteLine("Полученные данные:");
                    //string responseContent = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"\nUnitTest.SendPostRequest(): POST-запрос по адресу {uri} прошёл неудачно!");
                    Console.WriteLine($"Статус ошибки: {response.StatusCode}");
                    Console.WriteLine($"Причина: {response.ReasonPhrase}");
                    Console.WriteLine($"Переданные данные: {jsonContent}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Task HandleRequest(HttpListenerRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}