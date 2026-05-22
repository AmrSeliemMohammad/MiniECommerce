using MiniECommerce.Products;
using System.Collections.Generic;

namespace MiniECommerce.Categories
{
    // I don't inherit from EntityDto here because I don't need the id.
    public class CreateUpdateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public List<CreateUpdateProductDto> Products { get; set; }
    }
}
