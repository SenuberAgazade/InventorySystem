using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStocksController : ControllerBase
    {
        IProductStockService _productStockService;

        public ProductStocksController(IProductStockService productStockService)
        {
            _productStockService = productStockService;
        }

        [HttpGet("getallstock")]
        public IActionResult GetAll()
        {
            var result = _productStockService.GetAllStock();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getstock")]
        public IActionResult GetStock(int productId)
        {
            var result = _productStockService.GetStock(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("addstock")]
        public IActionResult AddStock(int productId, int newAddedProductCount)
        {
            var result = _productStockService.AddStock(productId, newAddedProductCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("removestock")]
        public IActionResult RemoveStock(int productId, int newSoldProductCount)
        {
            var result = _productStockService.RemoveStock(productId, newSoldProductCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
