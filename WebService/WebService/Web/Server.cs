using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WebService
{
    public class Server
    {
        public static async Task ListenAsync(IService service)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(service.Uri);
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                await service.HandleRequest(request); // передача данных запроса сервису для обработки

                response.ContentLength64 = 0;
                response.Close();
            }
        }
    }
}