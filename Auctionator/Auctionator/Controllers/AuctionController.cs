using System;
using System.Threading.Tasks;
using Auctionator.Hubs;
using Auctionator.Services.Implementation;
using Auctionator.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Auctionator.Controllers
{
    [Route("auction")]
    public class AuctionController : Controller
    {
        readonly IHubContext<AuctionHub> _hubContext;
        private readonly IAuctionService _auctionService;

        public AuctionController(IHubContext<AuctionHub> hubContext, IAuctionService auctionService)
        {
            _hubContext = hubContext;
            _auctionService = auctionService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var auctions = await _auctionService.GetAll();
            return View(auctions);
        }

        [Route("{id:int}")]
        public IActionResult Detail(int id)
        {
            var auction = _auctionService.GetAuctionById(id);
            return View(auction);
        }
    }
}