using System;
using Auctionator.Enums;

namespace Auctionator.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public AuctionStatus Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double StartPrice { get; set; }
        public double LastBet { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string WinnerId { get; set; }
        public User Winner { get; set; }
    }
}
