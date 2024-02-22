using Markets.Contracts.Requests.Products;
using Markets.Contracts.Responses;

namespace Markets.Abstractions
{
    public interface IProductService
    {
        public int AddProduct(ProductCreateRequest product);
        public IEnumerable<ProductResponse> GetProducts();

        public ProductResponse GetProductById(int id);
        public bool DeleteProduct(int id);
        public bool UpdatePrice(int idProduct, int price);
        public bool DeleteCategory(string category);
    }
}
