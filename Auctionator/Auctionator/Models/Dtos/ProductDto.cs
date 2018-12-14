using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auctionator.Enums;

namespace Auctionator.Models.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public IList<string> PhotoPaths { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public ProductStatus Status { get; set; }
        public int AuctionId { get; set; }
        public string OwnerName { get; set; }
        public string BuyerName { get; set; }
    }
}
