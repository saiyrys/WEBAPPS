using Markets.Abstractions;
using Markets.Contracts.Responses;
using Markets.Models;

namespace Markets.GraphQL
{
    public class Query
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public Query(IProductService productSrevice, ICategoryService categoryService)
        {
            _productService = productSrevice;
            _categoryService = categoryService;
        }
        public IEnumerable<CategoryResponse> GetCategories() => _categoryService.GetCategories();
        public IEnumerable<ProductResponse> GetProducts() => _productService.GetProducts();
        public ProductResponse GetProductById(int id) => _productService.GetProductById(id);



    }
}
