using Markets.Models;
using System.ComponentModel.DataAnnotations;

namespace Markets.Contracts.Requests.Products
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public int? CategoryId { get; set; }


        public Product ProductGetEntity()
        {


            return new Product()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                CategoryId = CategoryId
            };
        }
    }

}
