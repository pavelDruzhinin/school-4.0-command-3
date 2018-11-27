using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auctionator.Models;

namespace Auctionator.Data.Mappings
{
    public class SubscribedAuctionMap : IEntityTypeConfiguration<SubscribedAuction>
    {
        public void Configure(EntityTypeBuilder<SubscribedAuction> builder)
        {
            builder.HasKey(x => new {x.UserId, x.AuctionId});
            builder.HasOne(x => x.User).WithMany(x => x.SubscribedAuctions).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Auction).WithMany(x => x.SubscribedAuctions).HasForeignKey(x => x.AuctionId);
        }
    }
}
