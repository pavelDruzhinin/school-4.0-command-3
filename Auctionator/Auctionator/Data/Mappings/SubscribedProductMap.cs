using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auctionator.Models;

namespace Auctionator.Data.Mappings
{
    public class SubscribedProductMap : IEntityTypeConfiguration<SubscribedProduct>
    {
        public void Configure(EntityTypeBuilder<SubscribedProduct> builder)
        {
            builder.HasKey(x => new {x.UserId, x.ProductId});
            builder.HasOne(x => x.User).WithMany(x => x.SubscribedProducts).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Product).WithMany(x => x.SubscribedProducts).HasForeignKey(x => x.ProductId);
        }
    }
}
