using MiniECommerce.Categories;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace MiniECommerce.Orders
{
    public class Order : AuditedAggregateRoot<Guid>
    {
        public Order() { }
        public Order(Guid id) : base(id) { }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
