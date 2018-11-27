using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Models
{
    public class User
    {
        public User()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public Owner Owner { get; set; }
        public Buyer Buyer { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}
