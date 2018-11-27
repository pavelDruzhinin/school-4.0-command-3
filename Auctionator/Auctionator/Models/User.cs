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
        /// <summary>
        /// Товары пользователя, выставленные на продажу
        /// </summary>
        public IList<Product> OwnProducts { get; set; }
        /// <summary>
        /// Товары, купленные пользователем
        /// </summary>
        public IList<Product> BoughtProducts { get; set; }
        /// <summary>
        /// Аукционы, выигранные пользователем
        /// </summary>
        public IList<Auction> WonAuctions { get; set; }
        /// <summary>
        /// Аукционы, на которые пользователь подписан
        /// </summary>
        public IList<SubscribedAuction> SubscribedAuctions { get; set; }
    }
}
