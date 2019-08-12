using Auction.Net.Data;
using Auction.Net.Models;
using System.Linq;

namespace Auction.Net.Repositories
{
    public class BidInfoRepository : IBidInfoRepository
    {
        private readonly ProductsDbContext _context;
        public BidInfoRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public int AddBidInfo(BidInfo bid)
        {
            bid.BidId = _context.Bids.OrderByDescending(p => p.BidId).FirstOrDefault()?.BidId?? 0 + 1;
            var newBid = _context.Bids.Add(bid);
            _context.SaveChanges();
            return newBid.Entity.BidId;
        }
    }
}
