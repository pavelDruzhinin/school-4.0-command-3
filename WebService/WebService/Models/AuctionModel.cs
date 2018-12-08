using System;

namespace WebService
{
    public class AuctionModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime EndPayDateTime { get; set; }
    }
}