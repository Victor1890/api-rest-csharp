using Microsoft.EntityFrameworkCore;
using web_app_rest.Models;

namespace web_app_rest.Context
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SuperMarket> SuperMarkets { get; set; }
        public DbSet<SuperMarketBrand> SuperMarketBrands { get; set; }
        public DbSet<ProductBrand> Productbrands { get; set; }
        public DbSet<Shop> Shops { get; set; }

    }
}
