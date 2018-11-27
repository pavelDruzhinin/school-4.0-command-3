using System.Collections.Generic;

namespace Auctionator.Models
{
    public class Winner
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public IList<Auction> Auctions { get; set; }
    }
}