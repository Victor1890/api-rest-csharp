using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace web_app_rest.Models
{
    [Table("products-brands")]
    public class ProductBrand
    {
        public int Id { get; set; }

        public Nullable<int> BrandId { get; set; }

        public Nullable<int> ProductId { get; set;}

        public int Stock { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        [JsonIgnore]
        public Brand? Brand { get; set; }
    }
}
