using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Auctionator.Models
{
    /// <summary>
    /// Класс User наследует все поля IdentityUser, в т.ч. UserName
    /// </summary>
    public class User : IdentityUser 
    {
        public Subscriber Subscriber { get; set; }

        public IList<Product> OwnProducts { get; set; }
        public IList<Product> BoughtProducts { get; set; }
        public IList<Auction> WonAuctions { get; set; }
    }
}
