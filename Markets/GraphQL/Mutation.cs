using Markets.Abstractions;
using Markets.Contracts.Requests.Categories;
using Markets.Contracts.Requests.Products;
using Markets.Contracts.Requests.Stores;

namespace Markets.GraphQL
{
    public class Mutation
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        public Mutation(IProductService productService, ICategoryService categoryService, IStoreService storeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _storeService = storeService;
        }

        public int AddProduct(ProductCreateRequest input) => _productService.AddProduct(input);
        public bool DeleteProduct(int id) => _productService.DeleteProduct(id);
        public bool UpdatePrice(int id, int price) => _productService.UpdatePrice(id, price);

        public int AddCategoriy(CreateCategoryRequest createCategoryRequest) => _categoryService.AddCategory(createCategoryRequest);

        public void AddProductToStore(AddProductsToStoreRequest request) =>
            _storeService.AddProductToStore(request.StoreId, request.Product.ProductGetEntity());

        public void UpdateProductStore(UpdateProductStoreRequest request) =>
            _storeService.UpdateProductStore(request.StoreId, request.ProductId, request.NewCount);

        public void RemoveProductFromStore(int storeId, int productId) =>
            _storeService.RemoveProductFromStore(storeId, productId);
    }
}
