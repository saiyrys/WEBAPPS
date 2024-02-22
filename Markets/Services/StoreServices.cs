using AutoMapper;
using Markets.Abstractions;
using Markets.Contexts;
using Markets.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace Markets.Services
{
    public class StoreService : IStoreService
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public StoreService(StoreContext storeContext, IMapper mapper, IMemoryCache memoryCache)
        {
            _storeContext = storeContext;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public void AddProductToStore(int storeId, Product product)
        {
            var store = _storeContext.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store != null)
            {
                store.Products.Add(product);
                _storeContext.SaveChanges();
                _memoryCache.Remove("products");
            }
        }

        public IEnumerable<Product> GetProductsInStore(int storeId)
        {
            var store = _storeContext.Stores.FirstOrDefault(s => s.Id == storeId);
            return store?.Products.ToList() ?? new List<Product>();
        }

        public void RemoveProductFromStore(int storeId, int productId)
        {
            var store = _storeContext.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store != null)
            {
                var productToRemove = store.Products.FirstOrDefault(p => p.Id == productId);
                if (productToRemove != null)
                {
                    store.Products.Remove(productToRemove);
                    _storeContext.SaveChanges();
                    _memoryCache.Remove("products");
                }
            }
        }

        public void UpdateProductStore(int storeId, int productId, int newCount)
        {
            var store = _storeContext.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store != null)
            {
                var productToUpdate = store.Products.FirstOrDefault(p => p.Id == productId);
                if (productToUpdate != null)
                {
                    productToUpdate.Count = newCount;
                    _storeContext.SaveChanges();
                }
            }
        }
    }
}
