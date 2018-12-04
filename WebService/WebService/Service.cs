using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;

namespace WebService
{
    public class Service : IService
    {
        public string Uri { get; }
        private readonly string _dbConnectionString;
        private readonly string _startDateTimeUri;
        private readonly string _endDateTimeUri;
        private readonly string _endPayDateTimeUri;
        private bool _isUpdated = false; // обновлены ли данные словарей?

        public SortedDictionary<DateTime, List<int>> StartIds; // словарь <Дата и время начала аукциона, Id всех аукционов с этой датой и временем>
        public SortedDictionary<DateTime, List<int>> EndIds;
        public SortedDictionary<DateTime, List<int>> EndPayIds;

        public Service()
        {
            // инициализация конфигурации
            Uri = ConfigurationManager.ConnectionStrings["ApplicationUri"].ConnectionString;
            _dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            _startDateTimeUri = ConfigurationManager.ConnectionStrings["StartDateTimeUri"].ConnectionString;
            _endDateTimeUri = ConfigurationManager.ConnectionStrings["EndDateTimeUri"].ConnectionString;
            _endPayDateTimeUri = ConfigurationManager.ConnectionStrings["EndPayDateTimeUri"].ConnectionString;

            // инициализация словарей
            StartIds = new SortedDictionary<DateTime, List<int>>();
            EndIds = new SortedDictionary<DateTime, List<int>>();
            EndPayIds = new SortedDictionary<DateTime, List<int>>();
        }

        protected async Task GetDataFromDb()
        {
            IEnumerable<AuctionModel> auctions;

            using (IDbConnection db = new SqlConnection(_dbConnectionString))
            {
                var currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string query = "SELECT Id, StartDateTime, EndDateTime, EndPayDateTime FROM Auctions " +
                               $"WHERE EndPayDateTime >= '{currentDateTime}'";
                auctions = await db.QueryAsync<AuctionModel>(query);
            }

            // добавление ключей в словари
            foreach (var auc in auctions)
            {
                if (!StartIds.ContainsKey(auc.StartDateTime))
                    StartIds.Add(auc.StartDateTime, new List<int>());
                if (!EndIds.ContainsKey(auc.EndDateTime))
                    EndIds.Add(auc.EndDateTime, new List<int>());
                if (!EndPayIds.ContainsKey(auc.EndPayDateTime))
                    EndPayIds.Add(auc.EndPayDateTime, new List<int>());
            }

            // добавление значений в словари
            foreach (var auc in auctions)
            {
                StartIds[auc.StartDateTime].Add(auc.Id);
                EndIds[auc.EndDateTime].Add(auc.Id);
                EndPayIds[auc.EndPayDateTime].Add(auc.Id);
            }
        }

        /// <summary>
        /// Старт работы сервиса
        /// </summary>
        public async void Run()
        {
            await GetDataFromDb(); // в первую очередь нужно получить данные из БД, если ещё действительны

            var startDateTime = StartIds.FirstOrDefault();
            var endDateTime = EndIds.FirstOrDefault();
            var endPayDateTime = EndPayIds.FirstOrDefault();

            while (true)
            {
                DateTime currentDateTime = DateTime.Now;
                if (_isUpdated) // проверка, обновился ли словарь (это может произойти после обработки запроса в HadnleRequest())
                {
                    startDateTime = StartIds.FirstOrDefault();
                    endDateTime = EndIds.FirstOrDefault();
                    endPayDateTime = EndPayIds.FirstOrDefault();
                    _isUpdated = false;
                }
                if (startDateTime.Key <= currentDateTime &&
                    startDateTime.Key != default(DateTime))
                {
                    await SendRequestAsync(_startDateTimeUri, startDateTime.Value);
                    StartIds.Remove(startDateTime.Key);
                    startDateTime = StartIds.FirstOrDefault();
                }
                if (endDateTime.Key <= currentDateTime &&
                    endDateTime.Key != default(DateTime))
                {
                    await SendRequestAsync(_endDateTimeUri, endDateTime.Value);
                    EndIds.Remove(endDateTime.Key);
                    endDateTime = EndIds.FirstOrDefault();
                }
                if (endPayDateTime.Key <= currentDateTime &&
                    endPayDateTime.Key != default(DateTime))
                {
                    await SendRequestAsync(_endPayDateTimeUri, endPayDateTime.Value);
                    EndPayIds.Remove(endPayDateTime.Key);
                    endPayDateTime = EndPayIds.FirstOrDefault();
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Отправка запроса серверу
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="idList"></param>
        /// <returns></returns>
        protected async Task SendRequestAsync(string uri, IList<int> idList)
        {
            string jsonContent = JsonConvert.SerializeObject(idList); // формиование JSON
            var response = await Client.RequestPostAsync(uri, jsonContent); // отправка запроса, получение ответа
            await HandleResponse(uri, response); // обработка ответа
        }

        /// <summary>
        /// Обработка ответа от сервера
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected async Task HandleResponse(string uri, HttpResponseMessage response)
        {
            var jsonType = new { success = false, result = "" };
            var jsonResponseContent = await response.Content.ReadAsStringAsync();

            try
            {
                var responseContent = JsonConvert.DeserializeAnonymousType(jsonResponseContent, jsonType);
                if (responseContent == null)
                    throw new Exception();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine($"\nЗапрос по адресу {uri} прошёл успешно!");
                    Console.WriteLine("Полученные данные");
                    Console.WriteLine(responseContent.result);
                    //Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nЗапрос по адресу {uri} прошёл неудачно! Код ошибки: {response.StatusCode}");
                Console.WriteLine("Причина ошибки:");
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine(e);
                //Console.WriteLine();
            }
        }

        /// <summary>
        /// Обработка запроса от сервера
        /// </summary>
        /// <param name="request">Объект запроса</param>
        /// <returns></returns>
        public async Task HandleRequest(HttpListenerRequest request)
        {
            var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string requestContent = await reader.ReadToEndAsync();
            AuctionModel auction = JsonConvert.DeserializeObject<AuctionModel>(requestContent);
            if (auction == null)
            {
                Console.WriteLine("Service.HandleRequest(): модель AuctionModel == null!");
                //Console.WriteLine();
            }
            else
            {
                AddDateTime(auction);
                Console.WriteLine("Service.HandleRequest(): полученные данные AuctionModel:");
                Console.WriteLine($"{auction.Id}");
                Console.WriteLine($"{auction.StartDateTime}");
                Console.WriteLine($"{auction.EndDateTime}");
                Console.WriteLine($"{auction.EndPayDateTime}");
                //Console.WriteLine();
            }
        }

        /// <summary>
        /// Добавление дат и времени в словари
        /// </summary>
        /// <param name="auction"></param>
        protected void AddDateTime(AuctionModel auction)
        {
            if (!StartIds.ContainsKey(auction.StartDateTime))
                StartIds.Add(auction.StartDateTime, new List<int>());
            if (!EndIds.ContainsKey(auction.EndDateTime))
                EndIds.Add(auction.EndDateTime, new List<int>());
            if (!EndPayIds.ContainsKey(auction.EndPayDateTime))
                EndPayIds.Add(auction.EndPayDateTime, new List<int>());

            StartIds[auction.StartDateTime].Add(auction.Id);
            EndIds[auction.EndDateTime].Add(auction.Id);
            EndPayIds[auction.EndPayDateTime].Add(auction.Id);

            _isUpdated = true;
        }
    }
}