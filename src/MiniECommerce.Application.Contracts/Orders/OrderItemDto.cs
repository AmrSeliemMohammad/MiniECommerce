using MiniECommerce.Products;
using System;
using Volo.Abp.Application.Dtos;

namespace MiniECommerce.Orders
{
    public class OrderItemDto : EntityDto<Guid>
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
