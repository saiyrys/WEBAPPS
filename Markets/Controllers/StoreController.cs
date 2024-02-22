using Markets.Models;
using Markets.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Markets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost("AddProductToStore")]
        public IActionResult AddProductToStore(int storeId, Product product)
        {
            _storeService.AddProductToStore(storeId, product);
            return Ok();
        }

        [HttpPut("UpdateProductStore")]
        public IActionResult UpdateProductCountInStore(int storeId, int productId, int newCount)
        {
            _storeService.UpdateProductStore(storeId, productId, newCount);
            return Ok();
        }
        [HttpDelete("RemoveProductFromStore")]
        public IActionResult RemoveProductFromStore(int storeId, int productId)
        {
            _storeService.RemoveProductFromStore(storeId, productId);
            return Ok();
        }

    }
}
