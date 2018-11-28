using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctionator.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Auctionator.Controllers
{
    public class AuctionController : Controller
    {
        readonly IHubContext<AuctionHub> hubContext;

        public AuctionController(IHubContext<AuctionHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string product)
        {
            await hubContext.Clients.All.SendAsync("Notify", $"Добавлено: {product} - {DateTime.Now.ToShortTimeString()}");
            return RedirectToAction("Index");
        }
    }
}