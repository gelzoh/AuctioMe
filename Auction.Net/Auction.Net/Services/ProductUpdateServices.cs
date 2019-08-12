using Microsoft.AspNetCore.SignalR;

namespace Auction.Net.Services
{
    public class ProductUpdateServices : Hub
    {
        public void SendToGroup(string userName, string bid, string productId)
        {
            Clients.All.SendAsync(productId, userName, bid);
        }
    }
}
