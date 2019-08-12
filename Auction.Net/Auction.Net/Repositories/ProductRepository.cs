using System.Collections.Generic;
using System.Linq;
using Auction.Net.Data;
using Auction.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.Net.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext _context;

        public ProductRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public Product GetProduct(int productId)
        {
            return _context.Products
                .Include(p => p.LastBid)
                .FirstOrDefault(p=> p.ProductId == productId);
        }

        public List<Product> GetProducts()
        {
            return _context.Products
                .Include(p => p.LastBid)
                .ToList();
        }
    }
}
