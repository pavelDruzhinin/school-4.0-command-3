using System;

namespace Auctionator.Models.Dtos
{
    public class BetDto
    {
        public double CurrentBet { get; set; }
        public int ProductId { get; set; }
        public int AuctionId { get; set; }
        public string UserName { get; set; }
        public DateTime BetDateTime { get; set; }
    }
}
