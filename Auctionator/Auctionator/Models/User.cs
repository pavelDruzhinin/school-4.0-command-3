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
        public Owner Owner { get; set; }
        public Buyer Buyer { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}
