using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace WebService
{
    public class Service
    {
        private readonly string _dbConnectionString;
        private readonly string _startDateTimeUri;
        private readonly string _endDateTimeUri;
        private readonly string _endPayDateTimeUri;

        public SortedDictionary<DateTime, List<int>> StartIds; // словарь <Дата и время начала аукциона, Id всех аукционов с этой датой и временем>
        public SortedDictionary<DateTime, List<int>> EndIds;
        public SortedDictionary<DateTime, List<int>> EndPayIds;

        public Service()
        {
            // инициализация конфигурации
            _dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            _startDateTimeUri = ConfigurationManager.ConnectionStrings["StartDateTimeUri"].ConnectionString;
            _endDateTimeUri = ConfigurationManager.ConnectionStrings["EndDateTimeUri"].ConnectionString;
            _endPayDateTimeUri = ConfigurationManager.ConnectionStrings["EndPayDateTimeUri"].ConnectionString;

            // инициальизация словарей
            StartIds = new SortedDictionary<DateTime, List<int>>(); 
            EndIds = new SortedDictionary<DateTime, List<int>>();
            EndPayIds = new SortedDictionary<DateTime, List<int>>();
            GetDataFromDb();
        }

        private void GetDataFromDb()
        {
            List<AuctionModel> auctions;

            using (IDbConnection db = new SqlConnection(_dbConnectionString))
            {
                var currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string query = "SELECT Id, StartDateTime, EndDateTime, EndPayDateTime FROM Auctions " +
                               $"WHERE EndPayDateTime >= '{currentDateTime}'";
                auctions = db.Query<AuctionModel>(query).ToList();
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
        public async Task Run()
        {
            var startDateTime = StartIds.FirstOrDefault();
            var endDateTime = EndIds.FirstOrDefault();
            var endPayDateTime = EndPayIds.FirstOrDefault();

            while (true)
            {
                if (Console.ReadKey(false).KeyChar == 'a')
                {
                    await ClientRole.RequestAsync(_startDateTimeUri, startDateTime.Value);
                    StartIds.Remove(startDateTime.Key);
                    startDateTime = StartIds.FirstOrDefault();
                }
                if (endDateTime.Key == DateTime.Now)
                {
                    await ClientRole.RequestAsync(_endDateTimeUri, endDateTime.Value);
                    EndIds.Remove(endDateTime.Key);
                    endDateTime = EndIds.FirstOrDefault();
                }
                if (endPayDateTime.Key == DateTime.Now)
                {
                    await ClientRole.RequestAsync(_endPayDateTimeUri, endPayDateTime.Value);
                    EndPayIds.Remove(endPayDateTime.Key);
                    endPayDateTime = EndPayIds.FirstOrDefault();
                }
                Thread.Sleep(700);
            }
        }

    }
}