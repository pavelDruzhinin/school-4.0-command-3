using Auctionator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Data.Mappings
{
    public class BetMap : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CurrentBet).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.BetDateTime).IsRequired();

            builder.HasOne(x => x.Auction).WithMany(x => x.Bets).HasForeignKey(x => x.AuctionId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
