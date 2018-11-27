using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Models
{
    public class SubscribedAuction 
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
    }
}