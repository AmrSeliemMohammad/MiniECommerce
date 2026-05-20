using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace MiniECommerce.Categories
{
    public class Category : AuditedEntity<int>
    {   
            public string Name { get; set; }
            public string Description { get; set; }
            public int? ParentId { get; set; }
            public virtual Category Parent { get; set; }
            public virtual ICollection<Category> Children { get; set; }
        }
    }
