using Microsoft.AspNetCore.Authorization;
using MiniECommerce.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace MiniECommerce.Orders
{
    [Authorize]
    public class OrdersAppService : ApplicationService, IOrdersAppService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Product, int> _productRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IDistributedEventBus _distributedEventBus;
        public OrdersAppService(IRepository<Order, Guid> orderRepository, ICurrentUser user, IRepository<Product, int> productRepository, IDistributedEventBus distributedEventBus)
        {
            _orderRepository = orderRepository;
            _currentUser = user;
            _productRepository = productRepository;
            _distributedEventBus = distributedEventBus;
        }
        public async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _orderRepository.WithDetailsAsync();

            if (_currentUser.IsInRole(MiniECommerceConsts.CustomerRole) && !_currentUser.IsInRole(MiniECommerceConsts.AdminRole))
            {
                query = query.Where(o => o.CreatorId == _currentUser.Id);
            }
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            int totalCount;
            if (_currentUser.IsInRole(MiniECommerceConsts.CustomerRole) && !_currentUser.IsInRole(MiniECommerceConsts.AdminRole))
            {
                totalCount = await _orderRepository.CountAsync(o => o.CreatorId == _currentUser.Id);
            }
            else {                
                totalCount = await _orderRepository.CountAsync();
            }

            var orders = await AsyncExecuter.ToListAsync(query);

            var orderDtos = ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);

            return new PagedResultDto<OrderDto>(totalCount, orderDtos);
        }

        public async Task<Guid> CreateAsync(CreateUpdateOrderDto input)
        {
            _productRepository.GetListAsync(p => input.Items.Select(i => i.ProductId).Contains(p.Id)).Result.ToList().ForEach(p =>
             {
                 var item = input.Items.First(i => i.ProductId == p.Id);
                 if (item.Quantity > p.StockQuantity)
                 {
                     throw new OrderQuantityException();
                 }
             });    
            var order = ObjectMapper.Map<CreateUpdateOrderDto, Order>(input);
            order = await _orderRepository.InsertAsync(order);

            await _distributedEventBus.PublishAsync(
                input.Items.Select(i => new StockCountChangedEvent
                {
                    ProductId = i.ProductId,
                    OrderQuantity = i.Quantity
                })
            );

            return order.Id;
        }

        public async Task<OrderDto> GetAsync(Guid id)
        {
            Order order;
            if (_currentUser.IsInRole(MiniECommerceConsts.CustomerRole) && !_currentUser.IsInRole(MiniECommerceConsts.AdminRole))
            {
                var query = _orderRepository.WithDetailsAsync().Result;
                query = query.Where(o => o.Id == id && o.CreatorId == _currentUser.Id);
                
                var filteredOrder = await AsyncExecuter.FirstOrDefaultAsync(query);
                if (filteredOrder == null)
                {
                    throw new EntityNotFoundException(typeof(Order), id);
                }
                order = filteredOrder;
            }
            else
            {
                order = await _orderRepository.GetAsync(id);
            }

            return ObjectMapper.Map<Order, OrderDto>(order);
        }
    }
}
