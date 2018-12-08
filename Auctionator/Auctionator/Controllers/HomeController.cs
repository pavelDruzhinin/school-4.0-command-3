using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auctionator.Models;
using Newtonsoft.Json.Linq;

namespace Auctionator.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetProduct()
        {
            var id = this.Request.Query["id"];
            string jsonData;
            switch (id)
            {
                case "1":
                    jsonData = @"{  
                'startPrice':'234',  
                'startDate':'12.12.2018',
                'lastBet':'1234567'
                }";
                    break;

                case "2":
                    jsonData = @"{  
                'startPrice':'23124',  
                'startDate':'1.12.2018',
                'lastBet':'0'
                }";
                    break;

                case "3":
                    jsonData = @"{  
                'startPrice':'111',  
                'startDate':'08.12.2018',
                'lastBet':'111111111'
                }";
                    break;
                default:
                    jsonData = @"{  
                'startPrice':'90',  
                'startDate':'22.12.2018',
                'lastBet':'0'
                }";
                    break;
            }

            var details = JObject.Parse(jsonData);
            
            return Json(details);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
