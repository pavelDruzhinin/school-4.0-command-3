using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;

namespace Auctionator.Hubs
{
    public class AuctionHub : Hub
    {
        /// <summary>
        /// Словарь: Id пользователя - группа
        /// </summary>
        public IDictionary<string, string> UserGroupDict { get; set; }

        //public IDictionary<string, string> UserConnDict { get; set; }

        private readonly IAuctionService _auctionService;

        public AuctionHub(IAuctionService auctionService)
        {
            _auctionService = auctionService;
            UserGroupDict = new Dictionary<string, string>();
            //UserConnDict = new Dictionary<string, string>();
        }

        public async Task JoinGroup(string groupName)
        {
            var userId = Context.User.Identity.Name;
            var connId = Context.ConnectionId;

            await Groups.AddToGroupAsync(connId, groupName);

            //UserConnDict.Add(userId, connId);
            UserGroupDict.Add(userId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            var userId = Context.User.Identity.Name;
            var connId = Context.ConnectionId;

            await Groups.RemoveFromGroupAsync(connId, groupName);

            //UserConnDict.Remove(userId);
            UserGroupDict.Remove(userId);
        }

        //public override async Task OnConnectedAsync()
        //{
        //    var userId = Context.User.Identity.Name;
        //    var connId = Context.ConnectionId;

        //    // Если пользователь относится к этой группе, то добавляем его в системную группу при подключении
        //    if (UserGroupDict.ContainsKey(userId))
        //    {
        //        await Groups.AddToGroupAsync(connId, UserGroupDict[userId]);
        //    }
        //   // UserConnDict[userId] = connId; // Запоминаем его новый ConnectionId
        //    await base.OnConnectedAsync();
        //}

        public async Task Send(string groupName, double currentBet, string userName)
        {
            BetDto betDto = new BetDto()
            {
                ProductId = Convert.ToInt32(groupName),
                BetDateTime = DateTime.Now,
                CurrentBet = currentBet,
                UserName = userName
            };
            try
            {
                await _auctionService.AddBet(betDto, Context.User.Identity.Name);

                await Clients.Group(groupName).SendAsync("GetBet", betDto);

            }
            catch (Exception e)
            {
                await Clients.Group(groupName).SendAsync("GetBet", e);
            }
        }
    }
}
