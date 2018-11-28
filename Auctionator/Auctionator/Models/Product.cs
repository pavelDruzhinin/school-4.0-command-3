using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctionator.Enums;

namespace Auctionator.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IList<ProductPhoto> Photos { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public ProductStatus Status { get; set; }

        public IList<SubscribedProduct> SubscribedProducts { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public string BuyerId { get; set; }
        public User Buyer { get; set; }
    }
}
