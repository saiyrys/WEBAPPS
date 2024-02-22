using Microsoft.AspNetCore.Mvc;
using Markets.Models;
using Markets.Contexts;
using Markets.Contracts.Requests;
using Markets.Contracts.Responses;
using System.Collections.Generic;
using System.Linq;
using System;
using Markets.Contracts.Requests.Products;

namespace Markets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private StoreContext _storeContext;
        private static readonly string _cacheStatsFilePath = "cache_stats.txt";
        private static int totalRequests = 0;
        private static int cacheHits = 0;
        private static int cacheMisses = 0;
        private static Dictionary<int, Product> cache = new Dictionary<int, Product>();
        private static string cacheStatsFilePath = "cache_stats.txt";

        public ProductsController(StoreContext context)
        {
            _storeContext = context;
        }

        [HttpGet]
        [Route("products/{id}")]
        public ActionResult<ProductResponse> GetProduct(int id)
        {
            totalRequests++;

            if (cache.ContainsKey(id))
            {
                cacheHits++;
                return Ok(new ProductResponse(cache[id]));
            }
            else
            {
                var result = _storeContext.Products.FirstOrDefault(p => p.Id == id);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    cacheMisses++;
                    cache[id] = result;
                    WriteCacheStatsToFile();
                    return Ok(new ProductResponse(result));
                }
            }
        }

        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<ProductResponse>> GetProducts()
        {
            var result = _storeContext.Products;

            return Ok(result.Select(result => new ProductResponse(result)));
        }

        [HttpPost]
        [Route("products")]
        public ActionResult<ProductResponse> AddProducts(ProductCreateRequest request)
        {
            Product product = request.ProductGetEntity();
            try
            {
                var result = _storeContext.Products.Add(product).Entity;

                _storeContext.SaveChanges();
                return Ok(new ProductResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("products/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult DeleteProduct(int id)
        {
            var product = _storeContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _storeContext.Products.Remove(product);
            _storeContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("categories/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCategory(int id)
        {
            var category = _storeContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _storeContext.Products.RemoveRange(_storeContext.Products.Where(p => p.CategoryId == id));

            _storeContext.Categories.Remove(category);
            _storeContext.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("products/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult SetProductPrice(int id, [FromBody] decimal price)
        {
            var existingProduct = _storeContext.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Price = price;

            _storeContext.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        [Route("cache-stats")]
        public IActionResult GetCacheStatsFile()
        {
            if (System.IO.File.Exists(cacheStatsFilePath))
            {
                var cacheStats = System.IO.File.ReadAllText(cacheStatsFilePath);
                return Ok(cacheStats);
            }
            else
            {
                return NotFound("Файл кэша не найден");
            }
        }

        [HttpGet]
        [Route("cache-stats-file")]
        public IActionResult GetCacheStatsFileContent()
        {
            WriteCacheStatsToFile();

            if (System.IO.File.Exists(cacheStatsFilePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(cacheStatsFilePath);
                return File(fileBytes, "text/plain", "cache_stats.txt");
            }
            else
            {
                return NotFound("Файл кэша не найден");
            }
        }

        private static void WriteCacheStatsToFile()
        {
            string cacheStats = $"Общее количество запросов: {totalRequests}";

            System.IO.File.WriteAllText(cacheStatsFilePath, cacheStats);
        }

    }
}
