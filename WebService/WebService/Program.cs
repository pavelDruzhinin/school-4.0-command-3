using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;

namespace WebService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var service = new Service();

            Task.Run(() => service.Run()); // запуск сервиса
            Task.Run(() => Server.ListenAsync(service)); // запуск сервера

            while (true)
            { Thread.Sleep(500); }

        }
    }
}
