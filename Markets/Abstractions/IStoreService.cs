using Markets.Models;
using System.Collections.Generic;

namespace Markets.Abstractions
{
    public interface IStoreService
    {
        IEnumerable<Product> GetProductsInStore(int storeId);
        void AddProductToStore(int storeId, Product product);
        void RemoveProductFromStore(int storeId, int productId);
        void UpdateProductStore(int storeId, int productId, int newCount);
    }
}
