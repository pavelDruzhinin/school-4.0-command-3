using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctionator.Enums;

namespace Auctionator.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public AuctionStatus Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime EndPayDateTime { get; set; }
        public double StartPrice { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string WinnerId { get; set; } // ���������� �� ������ ������
        public User Winner { get; set; }
        public IList<Bet> Bets { get; set; } // ���� ������
    }
}
