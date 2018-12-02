using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Auctionator.Hubs;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        /// <summary>
        /// запуск аукционов
        /// </summary>
        /// <param name="auctionIds">список Id запускаемых аукционов</param>
        /// <returns></returns>
        [Route("start")]
        public async Task<JsonResult> StartAuctions([FromBody] IList<int> auctionIds)
        {
            try
            {
                int a = auctionIds.First();
                // изменить в БД статус на Active тех аукционов, у которых статус Wait и Id из списка auctionIds
                return Json(new { success = true, result = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
            
        }

        /// <summary>
        /// список аукционов по "статусу", JSON
        /// </summary>
        /// <param name="status">Статус аукциона</param>
        /// <returns></returns>
        [Route("getall")]
        public async Task<JsonResult> GetAuctions(Enums.AuctionStatus status)
        {
            try
            {
                var auctions = await _auctionService.GetAll(status);
                return Json(new { success = true, result = auctions });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        /// <summary>
        /// Список аукционов пользователя, где он победил
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns></returns>
        public async Task<JsonResult> GetUserAuctions(string userId)
        {
            try
            {
                var auctions = await _auctionService.GetAuctionsByUser(userId);
                return Json(new { success = true, result = auctions });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }
        
        /// <summary>
        /// Детали аукциона (так же используется для вывода при редактировании параметров аукциона), результат JSON
        /// </summary>
        /// <param name="id">Id аукциона</param>
        /// <returns></returns>
        [Route("detail/{id:int}")]
        public async Task<JsonResult> Detail(int id)
        {
            try
            {
                var auction = await _auctionService.GetAuctionById(id);
                return Json(new { success = true, result = auction });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        /// <summary>
        /// Получение списка аукционов для главной страницы, парметр count - сколько элементов надо получить, результат JSON
        /// </summary>
        /// <param name="count">Счетчик выводимых аукционов</param>
        /// <returns></returns>
        [Route("getformain")]
        public async Task<JsonResult> GetAuctionForMain(int count)
        {
            try
            {
                var auctions = await _auctionService.GetAuctionsForMainPage(count);
                return Json(new { success = true, result = auctions });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        /// <summary>
        /// Post метод добавления товара на аукцион, получаем JSON
        /// </summary>
        /// <param name="auction">JSON для создания аукциона</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<JsonResult> Create(string auction)
        {
            AuctionDto auctionDto = JsonConvert.DeserializeObject<AuctionDto>(auction, new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy HH:mm:ss" }); 

            try
            {
                var auc = await _auctionService.Create(auctionDto);
                return Json(new { success = true, result = auc });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        /// <summary>
        /// Post метод изменения параметров аукциона, результат JSON
        /// </summary>
        /// <param name="auction">JSON для изменения параметров аукциона</param>
        /// <returns></returns>
        [HttpPost]
        [Route("update/{id:int}")]
        public async Task<JsonResult> Update(string auction, int id) // не учитывается статус аукциона для возможности редактирования
        {
            AuctionDto auctionDto = JsonConvert.DeserializeObject<AuctionDto>(auction, new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy HH:mm:ss" });

            try
            {
                var auc = await _auctionService.Update(auctionDto, id);
                return Json(new { success = true, result = auc });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        /// <summary>
        /// Ставка
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("addbet/{auctionId:int}")]
        public async Task<JsonResult> AddBet(string bet)
        {
            BetDto betDto = JsonConvert.DeserializeObject<BetDto>(bet);

            try
            {
                var currentBet = await _auctionService.AddBet(betDto);
                await _hubContext.Clients.All.SendAsync("Notify", $"Добавлена новая ставка: {betDto.CurrentBet} - {DateTime.Now.ToShortTimeString()}");
                return Json(new { success = true, result = currentBet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        /// <summary>
        /// Получение списка ставок для аукциона
        /// </summary>
        /// <param name="auctionId">Id аукциона</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbets/{auctionId:int}")]
        public async Task<JsonResult> GetBets(int auctionId)
        {
            try
            {
                var bets = await _auctionService.GetAllBets(auctionId);
                return Json(new { success = true, result = bets });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }
    }
}