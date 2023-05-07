using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_app_rest.Models
{
    [Table("supermarket-brands")]
    public class SuperMarketBrand
    {
        public int Id { get; set; }

        public Nullable<int> SuperMarketId { get; set; }

        public Nullable<int> BrandId { get; set; }

        [JsonIgnore]
        public SuperMarket? SuperMarket{ get; set; }

        [JsonIgnore]
        public Brand? Brand { get; set; }

    }
}
