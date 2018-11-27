using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Models
{
    public class Buyer 
    {
        public int UserId { get; set; }
        public List<Auction> Auctions { get; set; }
        public User User { get; set; }
    }
}