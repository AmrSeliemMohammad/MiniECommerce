using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MiniECommerce.Orders
{
    public class OrdersAppService : CrudAppService<Order, OrderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateOrderDto>, IOrdersAppService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        public OrdersAppService(IRepository<Order, Guid> repository) : base(repository)
        {
            _orderRepository = repository;
        }
        public override async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _orderRepository.WithDetailsAsync();
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var totalCount = await _orderRepository.CountAsync();

            var orders = await AsyncExecuter.ToListAsync(query);
                
            var orderDtos = ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);

            return new PagedResultDto<OrderDto>(totalCount, orderDtos);
        }
    }
}
