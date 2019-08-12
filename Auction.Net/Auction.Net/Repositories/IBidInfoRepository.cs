using Auction.Net.Models;

namespace Auction.Net.Repositories
{
    public interface IBidInfoRepository
    {
        int AddBidInfo(BidInfo bid);
    }
}
