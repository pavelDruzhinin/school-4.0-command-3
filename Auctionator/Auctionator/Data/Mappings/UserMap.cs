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
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany<Product>(x => x.BoughtProducts).WithOne(x => x.Buyer).HasForeignKey(x => x.BuyerId);
            builder.HasMany<Product>(x => x.OwnProducts).WithOne(x => x.Owner).HasForeignKey(x => x.OwnerId);
            builder.HasMany<Auction>(x => x.WonAuctions).WithOne(x => x.Winner).HasForeignKey(x => x.WinnerId);

        }
    }
}
