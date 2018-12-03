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

                var reader = new StreamReader(request.InputStream, request.ContentEncoding);

                string requestContent = await reader.ReadToEndAsync();
                Console.WriteLine($"Полученные данные:");

                await service.HandleRequest(requestContent); // передача данных запроса сервису для обработки

                string responseString = "<html><head><meta charset='utf8'></head><body>Привет мир!</body></html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}