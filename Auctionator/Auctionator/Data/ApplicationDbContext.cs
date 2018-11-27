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
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Winner> Winners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new OwnerMap());
            modelBuilder.ApplyConfiguration(new BuyerMap());
            modelBuilder.ApplyConfiguration(new SubscriberMap());
            modelBuilder.ApplyConfiguration(new WinnerMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new AuctionMap());
        }
    }
}
