﻿namespace Auctionator.Models
{
    public class SubscribedProduct
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
