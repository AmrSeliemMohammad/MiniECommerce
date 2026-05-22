using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MiniECommerce.Orders
{
    public interface IOrdersAppService : ICrudAppService<OrderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateOrderDto>
    {
    }
}
