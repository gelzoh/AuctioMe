using Auction.Net.Models;
using System.Collections.Generic;

namespace Auction.Net.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int productName);
        void UpdateProduct(Product product);
    }
}
