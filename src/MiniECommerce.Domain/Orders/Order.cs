using MiniECommerce.Categories;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace MiniECommerce.Orders
{
    public class Order : AuditedAggregateRoot<Guid>
    {
        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
