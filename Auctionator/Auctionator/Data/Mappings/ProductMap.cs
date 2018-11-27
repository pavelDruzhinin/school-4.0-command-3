using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auctionator.Models;

namespace Auctionator.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.HasOne(x => x.Owner).WithMany(x => x.Products).HasForeignKey(x => x.OwnerId);
            builder.HasOne(x => x.Buyer).WithMany(x => x.Products).HasForeignKey(x => x.BuyerId);
            builder.HasOne(x => x.Auction).WithOne(x => x.Product).HasForeignKey<Auction>(x => x.ProductId);

        }
    }
}
