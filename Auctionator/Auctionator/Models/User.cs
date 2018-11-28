using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Auctionator.Models
{
    /// <summary>
    /// ����� User ��������� ��� ���� IdentityUser, � �.�. UserName
    /// </summary>
    public class User : IdentityUser 
    {
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
