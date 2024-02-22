﻿using AutoMapper;
using Markets.Abstractions;
using Markets.Contracts.Requests.Products;
using Markets.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Markets.Contexts;
using Markets.Contracts.Responses;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Markets.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCash;


        public ProductService(StoreContext context, IMapper mapper, IMemoryCache memoryCash)
        {
            _storeContext = context;
            _mapper = mapper;
            _memoryCash = memoryCash;
        }
        public int AddProduct(ProductCreateRequest product)
        {
            var mapEntity = _mapper.Map<Product>(product);
            _storeContext.Products.Add(mapEntity);
            _storeContext.SaveChanges();
            _memoryCash.Remove("products");
            return mapEntity.Id;
        }

        public bool DeleteCategory(string category)
        {
            try
            {
                var deleteCategory = _storeContext.Products.Where(c => c.Description == category).FirstOrDefault();
                deleteCategory.Description = null;
                _storeContext.SaveChanges();
                _memoryCash.Remove("products");
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DeleteProduct(int id)
        {
            var deleteProduct = _storeContext.Products.FirstOrDefault(p => p.Id == id);
            if (deleteProduct != null)
            {
                _storeContext.Products.Remove(deleteProduct);
                _storeContext.SaveChanges();
                _memoryCash.Remove("products");
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProductResponse GetProductById(int id)
        {
            var product = _storeContext.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductResponse>(product);
        }

        public IEnumerable<ProductResponse> GetProducts()
        {
            if (_memoryCash.TryGetValue("products", out List<ProductResponse> result))
            {
                return result!;
            }
            IEnumerable<ProductResponse> products = _storeContext.Products.Select(_mapper.Map<ProductResponse>).ToList();

            _memoryCash.Set("products", products, TimeSpan.FromMinutes(30));

            return products;
        }

        public bool UpdatePrice(int idProduct, int price)
        {
            try
            {
                var product = _storeContext.Products.FirstOrDefault(c => c.Id == idProduct);
                product.Price = price;
                _storeContext.SaveChanges();
                _memoryCash.Remove("products");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
