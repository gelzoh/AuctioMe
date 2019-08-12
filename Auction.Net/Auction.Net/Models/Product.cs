using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Net.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int ProductId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("expirationDate")]
        public DateTime ExpirationDate { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("lastBid")]
        public BidInfo LastBid { get; set; }
    }
}
