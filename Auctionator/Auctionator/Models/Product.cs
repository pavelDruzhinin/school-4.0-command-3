using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public byte Status { get; set; } // TODO: enum для товаров

        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
        public List<Subscriber> Subscribers { get; set; }
    }
}
