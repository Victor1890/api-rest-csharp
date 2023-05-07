using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace web_app_rest.Models
{
    [Table("products")]
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; } = 0;

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public Collection<ProductBrand>? ProductBrands { get; set; }

        [JsonIgnore]
        public Collection<Shop>? Shops { get; set; }
    }
}
