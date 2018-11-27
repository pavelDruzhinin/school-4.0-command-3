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
        public Product Product { get; set; }
        public string Status { get; set; } // TODO: подумать нужен ли статус для аукциона или хватит только для товара
        public uint PurchasePrice { get; set; } // Пусть все цены будут целочисленными и больше 0, чтоб не запарно было
        public uint StartPrice { get; set; }
        public uint LastBet { get; set; } // последняя ставка на данный момент
        public List<Buyer> Buyers { get; set; } // покупатель на данный момент
        public string BuyerId { get; set; } 
        public DateTime RegDate { get; set; }
    }
}
