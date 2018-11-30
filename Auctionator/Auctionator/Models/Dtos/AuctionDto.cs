using Auctionator.Enums;
using System;

namespace Auctionator.Models.Dtos
{
    public class AuctionDto
    {
        public AuctionStatus Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public double StartPrice { get; set; }
        public int ProductId { get; set; }
    }
}
