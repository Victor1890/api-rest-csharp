using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_app_rest.Context;
using web_app_rest.Models;
using static web_app_rest.Interfaces.ShopInterface;

namespace web_app_rest.Controllers
{
    [Route("api/shops")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public ShopController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<object>>> CreateShoppingList([FromBody] ShopRequestPayload payload)
        {
            if (payload.superMarketId <= 0) return StatusCode(400, Problem("[superMarketId] property is mandatory", "", 400));
            if (payload.productId <= 0) return StatusCode(400, Problem("[productId] property is mandatory", "", 400));
            if (payload.stock <= 0) return StatusCode(400, Problem("[stock] property is mandatory", "", 400));

            if (_dbContext.Shops == null) return StatusCode(500, Problem("Something went wrong"));

            try
            {

                var shopFound = await _dbContext.Shops.Where(s => s.superMarketId == payload.superMarketId && s.productId == payload.productId).FirstOrDefaultAsync();
                if (shopFound != null) return StatusCode(400, Problem("This article exists"));

                var productBrandFound = await _dbContext.Productbrands.Where(p => payload.productId == p.ProductId).FirstOrDefaultAsync();
                if(productBrandFound == null) return StatusCode(404, Problem("This product doesn't exists", "", 404));

                var marketFound = await _dbContext.SuperMarkets.Where(sm => payload.superMarketId == sm.Id).FirstOrDefaultAsync();
                if(marketFound == null) return StatusCode(404, Problem("This market doesn't exists", "", 404));

                var stock = productBrandFound.Stock - payload.stock;

                if (stock <= 0) productBrandFound.Stock = 0;
                else productBrandFound.Stock = stock;

                _dbContext.Productbrands.Update(productBrandFound);

                _dbContext.Shops.Add(new Shop { productId = productBrandFound.Id, superMarketId = marketFound.Id });
                await _dbContext.SaveChangesAsync();

                return StatusCode(201, new { ok = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, Problem(ex.Message));
            }
        }
    }
}
