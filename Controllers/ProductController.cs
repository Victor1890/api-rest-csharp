using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using web_app_rest.Context;
using web_app_rest.Models;
using Microsoft.EntityFrameworkCore;
using static web_app_rest.Interfaces.ProductInterface;

namespace web_app_rest.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public ProductController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_dbContext.Products == null) return new JsonResult(new List<Product> { }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

            try
            {
                var list = await _dbContext.Products.OrderByDescending(b => b.Id).ToListAsync();
                if (list == null) return new JsonResult(new List<Product> { }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

                return new JsonResult(list, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProduct([FromBody] ProductRequestPayload payload)
        {
            if (payload.name == null) return StatusCode(400, Problem("[name] property is mandatory", "", 400));
            payload.description ??= "No description";

            if (_dbContext.Products == null) return StatusCode(500, Problem("Something went wrong"));

            try
            {
                _dbContext.Products.Add(new Product { Name = payload.name, Description = payload.description, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
                await _dbContext.SaveChangesAsync();

                return StatusCode(201, new { ok = "true" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }

        [HttpPost("/api/products/add-brand")]
        public async Task<ActionResult<IEnumerable<object>>> AddBrandProduct([FromBody] ProductBrandQuestPayload payload)
        {
            if (payload.brandId <= 0) return StatusCode(400, Problem("[brandId] property is mandatory", "", 400));
            if (payload.productId <= 0) return StatusCode(400, Problem("[brandId] property is mandatory", "", 400));
            if (payload.stock <= 0) return StatusCode(400, Problem("[stock] property is mandatory", "", 400));
            if (payload.price <= 0) return StatusCode(400, Problem("[price] property is mandatory", "", 400));

            if (_dbContext.Productbrands == null) return StatusCode(500, Problem("Something went wrong"));

            try
            {
                var productBrandFound = await _dbContext.Productbrands.Where(pb => payload.productId == pb.ProductId && payload.brandId == pb.BrandId).FirstOrDefaultAsync();
                if(productBrandFound != null) return StatusCode(400, Problem("This product does exists", "", 400));

                _dbContext.Productbrands.Add(new ProductBrand { BrandId = payload.brandId, ProductId = payload.productId, Stock = payload.stock});
                await _dbContext.SaveChangesAsync();

                var productFound = await _dbContext.Products.Where(p => p.Id == payload.productId).FirstOrDefaultAsync();
                if(productFound == null) return StatusCode(404, Problem("This product does exists", "", 404));

                productFound.Price = payload.price;
                productFound.UpdatedDate = DateTime.Now;

                _dbContext.Products.Update(productFound);
                await _dbContext.SaveChangesAsync();

                return StatusCode(200, new { ok = "true" });
            } catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }
    }
}
