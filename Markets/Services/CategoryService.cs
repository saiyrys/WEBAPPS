using AutoMapper;
using Markets.Abstractions;
using Markets.Contracts.Requests.Categories;
using Markets.Contracts.Responses;
using Markets.Models;
using Markets.Contexts;


namespace Markets.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _storeContext;

        public CategoryService(StoreContext storeContext, IMapper mapper)
        {
            _storeContext = storeContext;
            _mapper = mapper;
        }
        public int AddCategory(CreateCategoryRequest createCategoryRequest)
        {
            var mapEntity = _mapper.Map<Category>(createCategoryRequest);
            _storeContext.Categories.Add(mapEntity);
            _storeContext.SaveChanges();

            return mapEntity.Id;
        }

        public IEnumerable<CategoryResponse> GetCategories()
        {
            IEnumerable<CategoryResponse> products = _storeContext.Categories.Select(_mapper.Map<CategoryResponse>);

            return products;
        }
    }
}
