using AutoMapper;
using MiniECommerce.Categories;
using System.Linq;

namespace MiniECommerce.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ChildrenIds, opt => opt.MapFrom(src => src.Children.Select(ch => ch.Id)));
            CreateMap<CreateUpdateCategoryDto, Category>();
        }
    }
}
