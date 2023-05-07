using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using web_app_rest.Context;
using web_app_rest.Models;
using static web_app_rest.Interfaces.SuperMarketInterface;

namespace web_app_rest.Controllers
{
    [Route("api/super-market")]
    [ApiController]
    public class SuperMarketController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public SuperMarketController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperMarket>>> GetSuperMarket ()
        {
            if (_dbContext.SuperMarkets == null) return new JsonResult(new List<SuperMarket> { }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

            try
            {
                var list = await _dbContext.SuperMarkets.OrderByDescending(b => b.Id).ToListAsync();
                if (list == null) return new JsonResult(new List<SuperMarket> { }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

                return new JsonResult(list, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SuperMarket>>> CreateMarket([FromBody] SuperMarketRequestPayload payload)
        {
            if (payload.name == null) return StatusCode(400, Problem("[name] property is mandatory"));

            if (_dbContext.SuperMarkets== null) return StatusCode(500, Problem("Something went wrong"));

            try
            {
                _dbContext.SuperMarkets.Add(new SuperMarket { Name = payload.name, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
                await _dbContext.SaveChangesAsync();

                return StatusCode(201, new { ok = "true" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }

        [HttpPost("/api/super-market/add-brand")]
        public async Task<ActionResult<IEnumerable<object>>> AddMarketBrand([FromBody] SuperMarketBrandRequestPayload payload)
        {
            if (payload.superMarketId <= 0) return StatusCode(400, Problem("[superMarketId] property is mandatory", "", 400));
            if (payload.brandId <= 0) return StatusCode(400, Problem("[brandId] property is mandatory", "", 400));

            try
            {
                var superMarketBrandFound = await _dbContext.SuperMarketBrands.Where(smb => payload.superMarketId == smb.SuperMarketId && payload.brandId == smb.BrandId).FirstOrDefaultAsync();
                if(superMarketBrandFound != null) return StatusCode(400, Problem("This brand does exists", "", 400));

                _dbContext.SuperMarketBrands.Add(new SuperMarketBrand { SuperMarketId = payload.superMarketId, BrandId = payload.brandId });
                await _dbContext.SaveChangesAsync();

                return StatusCode(200, new { ok = "true" });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
