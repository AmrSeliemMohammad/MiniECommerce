using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MiniECommerce.Categories
{
    public class CategoryDto : AuditedEntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CategoryDto> Children { get; set; }
    }
}
