using Markets.Contracts.Requests.Categories;
using Markets.Contracts.Responses;

namespace Markets.Abstractions
{
    public interface ICategoryService
    {
        int AddCategory(CreateCategoryRequest createCategoryRequest);
        IEnumerable<CategoryResponse> GetCategories();
    }
}
