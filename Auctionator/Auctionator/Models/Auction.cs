using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; }
        public double PurchasingPrice { get; set; }
        public double StartingPrice { get; set; }
        public DateTime RegDate { get; set; }
    }
}
