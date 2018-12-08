using System.Net;
using System.Threading.Tasks;

namespace WebService
{
    public interface IService
    {
        /// <summary>
        /// URI самого сервиса
        /// </summary>
        string Uri { get; }
        /// <summary>
        /// Метод для запуска сервиса
        /// </summary>
        void Run();
        /// <summary>
        /// Метод для обработки запроса, пришедшего сервису
        /// </summary>
        Task HandleRequest(HttpListenerRequest request);
    }
}