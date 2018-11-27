using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auctionator.Models;

namespace Auctionator.Data.Mappings
{
    public class AuctionMap : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.StartDateTime).IsRequired();
            builder.Property(x => x.EndDateTime).IsRequired();
            builder.Property(x => x.StartPrice).IsRequired();

            builder.HasOne(x => x.Product).WithOne(x => x.Auction).HasForeignKey<Product>(x => x.AuctionId);
            builder.HasMany(x => x.Subscribers);
            builder.HasOne(x => x.Winner).WithMany(x => x.WonAuctions).HasForeignKey(x => x.WinnerId);
        }
    }
}
