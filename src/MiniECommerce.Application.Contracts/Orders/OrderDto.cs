using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MiniECommerce.Orders
{
    public class OrderDto : AuditedEntityDto<Guid>
    {
        public List<OrderItemDto> Items { get; set; }
    }
}
