using AutoMapper;
using Markets.Contracts.Requests.Categories;
using Markets.Contracts.Requests.Products;
using Markets.Contracts.Responses;
using Markets.Models;


namespace Markets.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductCreateRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductDeleteRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductUpdateRequest>(MemberList.Destination).ReverseMap();

            CreateMap<Category, CreateCategoryRequest>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryResponse>(MemberList.Destination).ReverseMap();
        }
    }
}
