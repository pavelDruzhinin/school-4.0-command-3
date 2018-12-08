using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public double CurrentBet { get; set; }
        public DateTime BetDateTime { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
    }
}
