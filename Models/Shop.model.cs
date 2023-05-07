using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_app_rest.Models
{
    [Table("shops")]
    public class Shop
    {
        public int Id { get; set; }
        public Nullable<int> productId { get; set;}
        public Nullable<int> superMarketId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        [JsonIgnore]
        public SuperMarket? SuperMarket { get; set; }
    }
}
