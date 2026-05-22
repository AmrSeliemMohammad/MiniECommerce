using MiniECommerce.Products;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace MiniECommerce.Categories
{
    public class Category : AuditedAggregateRoot<int>
    {   
            public string Name { get; set; }
            public string Description { get; set; }
            public int? ParentId { get; set; }
            public virtual Category Parent { get; set; }
            public virtual ICollection<Category> Children { get; set; }
            public virtual ICollection<Product> Products { get; set; }
    }
}
