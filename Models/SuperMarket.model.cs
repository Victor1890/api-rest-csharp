using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace web_app_rest.Models
{
    [Table("supermarkets")]
    public class SuperMarket
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public ICollection<SuperMarketBrand>? SuperMarketBrands { get; set; }

        [JsonIgnore]
        public ICollection<Shop>? Shops { get; set; }
    }
}
