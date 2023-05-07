using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_app_rest.Models
{
    [Table("brands")]
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public ICollection<SuperMarketBrand>? SuperMarketBrands { get; set; }

        [JsonIgnore]
        public ICollection<ProductBrand>? ProductBrands { get; set; }
    }
}
