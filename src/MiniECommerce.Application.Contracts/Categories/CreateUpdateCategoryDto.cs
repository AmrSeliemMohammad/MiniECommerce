using MiniECommerce.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniECommerce.Categories
{
    // I don't inherit from EntityDto here because I don't need the id.
    public class CreateUpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public List<CreateUpdateProductDto> Products { get; set; }
    }
}
