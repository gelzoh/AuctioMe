using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Net.Models
{
    public class BidInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BidId { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("maxBid")]
        public float MaxBid { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
