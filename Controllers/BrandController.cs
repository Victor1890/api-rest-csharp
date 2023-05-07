using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using web_app_rest.Context;
using web_app_rest.Models;
using static web_app_rest.Interfaces.BrandInterface;

namespace web_app_rest.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public BrandController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands ()
        {
            if (_dbContext.Brands == null) return new JsonResult(new List<Brand> { }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

            try
            {
                var list = await _dbContext.Brands.OrderByDescending(b => b.Id).ToListAsync();
                if(list  == null) return new JsonResult(new List<Brand> { }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

                return new JsonResult(list, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

            } catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Brand>>> GetOneBrand(int id)
        {
            if (_dbContext.Brands == null) return StatusCode(404, "Content not found");

            try
            {
                var brand = await _dbContext.Brands.FindAsync(id);
                if (brand == null) return new JsonResult(new { message = "Content not found" }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

                return StatusCode(200, brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Brand>>> CreateBrand([FromBody] BrandRequestPayload payload)
        {
            try
            {
                _dbContext.Brands.Add(new Brand { Name = payload.name, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
                await _dbContext.SaveChangesAsync();

                return StatusCode(201, new { ok = "true" });
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }
    }
}
