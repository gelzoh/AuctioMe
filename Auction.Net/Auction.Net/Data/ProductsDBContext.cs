using Auction.Net.Models;
using Microsoft.EntityFrameworkCore;
namespace Auction.Net.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<BidInfo> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BidInfo>()
                .Property(p => p.BidId)
                .ValueGeneratedOnAdd();
        }
    }
}
