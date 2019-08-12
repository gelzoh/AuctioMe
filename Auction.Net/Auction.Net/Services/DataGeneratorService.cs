using Auction.Net.Data;
using Auction.Net.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Auction.Net.Services
{
    public class DataGeneratorService
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductsDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProductsDbContext>>()))
            {
                // Look for any board games.
                if (context.Products.Any())
                {
                    return;   // Data was already seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        ProductId = 1,
                        Name = "Product 1",
                        ExpirationDate = DateTime.Now.AddDays(1),
                        Image = "https://via.placeholder.com/450x150?text=Product+1"
                    },
                    new Product
                    {
                        ProductId = 2,
                        Name = "Product 2",
                        ExpirationDate = DateTime.Now.AddMinutes(5),
                        Image = "https://via.placeholder.com/450x150?text=Product+2"
                    },
                    new Product
                    {
                        ProductId = 3,
                        Name = "Product 3",
                        ExpirationDate = DateTime.Now.AddHours(1),
                        Image = "https://via.placeholder.com/450x150?text=Product+3"
                    },
                    new Product
                    {
                        ProductId = 4,
                        Name = "Product 4",
                        ExpirationDate = DateTime.Now.AddDays(2),
                        Image = "https://via.placeholder.com/450x150?text=Product+4"
                    },
                    new Product
                    {
                        ProductId = 5,
                        Name = "Product 5",
                        ExpirationDate = DateTime.Now.AddDays(-1),
                        Image = "https://via.placeholder.com/450x150?text=Product+5"
                    },
                    new Product
                    {
                        ProductId = 6,
                        Name = "Product 6",
                        ExpirationDate = DateTime.Now.AddDays(1),
                        Image = "https://via.placeholder.com/450x150?text=Product+6",
                    });

                context.Bids.AddRange(
                    new BidInfo
                    {
                        BidId = 1,
                        MaxBid = 41,
                        UserName = "gelzoh",
                        ProductId = 3
                    },
                    new BidInfo
                    {
                        BidId = 2,
                        MaxBid = 150,
                        UserName = "gelzoh",
                        ProductId = 5
                    },
                    new BidInfo
                    {
                        BidId = 3,
                        MaxBid = 25,
                        UserName = "gelzoh",
                        ProductId = 6
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
