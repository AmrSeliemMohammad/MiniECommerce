using MiniECommerce.Products;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MiniECommerce.Categories
{
    public class CategoryDto : AuditedEntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> ChildrenIds { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
