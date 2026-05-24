using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MiniECommerce.Orders
{
    public interface IOrdersAppService
    {
        Task<Guid> CreateAsync(CreateUpdateOrderDto input);
        Task<OrderDto> GetAsync(Guid id);
        Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    }
}
