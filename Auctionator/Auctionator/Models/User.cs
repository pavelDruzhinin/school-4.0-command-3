using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Auctionator.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// ������ ������������, ������������ �� �������
        /// </summary>
        public IList<Product> OwnProducts { get; set; }
        /// <summary>
        /// ������, ��������� �������������
        /// </summary>
        public IList<Product> BoughtProducts { get; set; }
        /// <summary>
        /// ������, �� ������� ������������ ��������
        /// </summary>
        public IList<SubscribedProduct> SubscribedProducts { get; set; }
        /// <summary>
        /// ��������, ���������� �������������
        /// </summary>
        public IList<Auction> WonAuctions { get; set; }
    }
}
