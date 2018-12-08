using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Models.Dtos
{
    public class SubscribedProductDto
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
