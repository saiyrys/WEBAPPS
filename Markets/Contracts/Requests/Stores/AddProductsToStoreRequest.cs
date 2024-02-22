using Markets.Contracts.Requests.Products;

namespace Markets.Contracts.Requests.Stores
{
    public class AddProductsToStoreRequest
    {
        public int StoreId { get; set; }
        public ProductCreateRequest Product { get; set; }
    }
}
