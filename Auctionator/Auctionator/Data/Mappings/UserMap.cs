using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auctionator.Models;

namespace Auctionator.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Owner).WithOne(x => x.User).HasForeignKey<Owner>(x => x.UserId);
            builder.HasOne(x => x.Buyer).WithOne(x => x.User).HasForeignKey<Buyer>(x => x.UserId);
            builder.HasOne(x => x.Subscriber).WithOne(x => x.User).HasForeignKey<Subscriber>(x => x.UserId);
            builder.HasOne(x => x.Winner).WithOne(x => x.User).HasForeignKey<Winner>(x => x.UserId);
        }
    }
}
