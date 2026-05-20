using AutoMapper;
using MiniECommerce.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateUpdateCategoryDto, Category>();
        }
    }
}
