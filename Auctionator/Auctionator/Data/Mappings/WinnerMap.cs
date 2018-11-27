using Auctionator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auctionator.Data.Mappings
{
    public class WinnerMap : IEntityTypeConfiguration<Winner>
    {
        public void Configure(EntityTypeBuilder<Winner> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.HasOne(x => x.User).WithOne(x => x.Winner);
            builder.HasMany(x => x.Auctions).WithOne(x => x.Winner).HasForeignKey(x => x.WinnerId);
        }
    }
}