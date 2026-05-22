using AutoMapper;
using MiniECommerce.Products;

namespace MiniECommerce.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();
        }
    }
}
