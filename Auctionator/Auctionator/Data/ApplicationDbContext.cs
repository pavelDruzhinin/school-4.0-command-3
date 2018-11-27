using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Auctionator.Models;
using Auctionator.Data.Mappings;

namespace Auctionator.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<SubscribedAuction> SubscribedAuctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductPhotoMap());
            modelBuilder.ApplyConfiguration(new AuctionMap());
            modelBuilder.ApplyConfiguration(new SubscribedAuctionMap());
        }
    }
}
