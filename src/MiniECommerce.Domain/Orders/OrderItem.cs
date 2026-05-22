using MiniECommerce.Products;
using System;
using Volo.Abp.Domain.Entities;

namespace MiniECommerce.Orders
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
