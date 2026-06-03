
using MiniECommerce.Products;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Xunit;

namespace MiniECommerce.Orders
{
    public class OrdersAppService_Tests
    {
        private readonly IRepository<Order, Guid> _fakeOrderRepository;
        private readonly IRepository<Product, int> _fakeProductRepository;
        private readonly ICurrentUser _fakeCurrentUser;
        private readonly IDistributedEventBus _fakeDistributedEventBus;
        private readonly IObjectMapper _fakeObjectMapper;
        private readonly IAbpLazyServiceProvider _fakeAbpLazyServiceProvider;
        private readonly OrdersAppService _ordersAppService;
        private readonly IAsyncQueryableExecuter _fakeAsyncExecuter;
        public OrdersAppService_Tests()
        {
            _fakeOrderRepository = Substitute.For<IRepository<Order, Guid>>();
            _fakeProductRepository = Substitute.For<IRepository<Product, int>>();
            _fakeDistributedEventBus = Substitute.For<IDistributedEventBus>();
            _fakeCurrentUser = Substitute.For<ICurrentUser>();
            _fakeObjectMapper = Substitute.For<IObjectMapper>();
            _fakeAsyncExecuter = Substitute.For<IAsyncQueryableExecuter>();
            _fakeAbpLazyServiceProvider = Substitute.For<IAbpLazyServiceProvider>();

            _fakeAbpLazyServiceProvider.LazyGetService<IObjectMapper>(Arg.Any<Func<IServiceProvider, object>>()).Returns(_fakeObjectMapper);
            _fakeAbpLazyServiceProvider.LazyGetRequiredService<IAsyncQueryableExecuter>().Returns(_fakeAsyncExecuter);

            _ordersAppService = new OrdersAppService(
                _fakeOrderRepository,
                _fakeCurrentUser,
                _fakeProductRepository,
                _fakeDistributedEventBus
            )
            {
                LazyServiceProvider = _fakeAbpLazyServiceProvider
            };
        }

        [Fact]
        public async Task Should_Get_Async_Any_Order_If_Admin()
        {
            // Arrange
            _fakeCurrentUser.IsInRole(MiniECommerceConsts.AdminRole).Returns(true);

            var id = Guid.NewGuid();

            var order = new Order(id);

            _fakeOrderRepository.GetAsync(id).Returns(order);

            _fakeObjectMapper.Map<Order, OrderDto>(Arg.Any<Order>())
                .Returns(new OrderDto() { Id = id });

            // Act
            var orderDto = await _ordersAppService.GetAsync(id);

            // Assert
            await _fakeOrderRepository.Received(1).GetAsync(id);
            orderDto.Id.ShouldBe(id);
        }

        [Fact]
        public async Task Should_Get_Async_His_Order_If_Customer()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _fakeCurrentUser.IsInRole(MiniECommerceConsts.CustomerRole).Returns(true);
            _fakeCurrentUser.IsInRole(MiniECommerceConsts.AdminRole).Returns(false);
            _fakeCurrentUser.Id.Returns(userId);

            var orderId = Guid.NewGuid();
            var expectedOrder = new Order(orderId) { CreatorId = userId };

            var orders = new List<Order>() { expectedOrder }.AsQueryable();

            _fakeOrderRepository.WithDetailsAsync().Returns(orders);

            _fakeAsyncExecuter.FirstOrDefaultAsync(Arg.Any<IQueryable<Order>>())
                .Returns(callInfo =>
                {
                    var query = callInfo.Arg<IQueryable<Order>>();
                    var result = query.FirstOrDefault();
                    return Task.FromResult(result);
                });

            _fakeObjectMapper.Map<Order, OrderDto>(expectedOrder)
                .Returns(new OrderDto() { Id = orderId });

            // Act
            var orderDto = await _ordersAppService.GetAsync(orderId);

            // Assert
            await _fakeOrderRepository.Received(1).WithDetailsAsync();
            orderDto.Id.ShouldBe(orderId);
        }

        [Fact]
        public async Task Should_Throw_Exception_If_Order_Not_Found()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _fakeCurrentUser.IsInRole(MiniECommerceConsts.CustomerRole).Returns(true);
            _fakeCurrentUser.IsInRole(MiniECommerceConsts.AdminRole).Returns(false);
            _fakeCurrentUser.Id.Returns(userId);

            var orderId = Guid.NewGuid();
            var expectedOrder = new Order(orderId) { CreatorId = userId };

            var orders = new List<Order>() { expectedOrder }.AsQueryable();

            _fakeOrderRepository.WithDetailsAsync().Returns(orders);

            _fakeAsyncExecuter.FirstOrDefaultAsync(Arg.Any<IQueryable<Order>>())
                .ReturnsNull();

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _ordersAppService.GetAsync(orderId));
            await _fakeOrderRepository.Received(1).WithDetailsAsync();
        }

        [Fact]
        public async Task Should_Get_List_Async_All_Orders_If_Admin()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _fakeCurrentUser.IsInRole(MiniECommerceConsts.AdminRole).Returns(true);
            _fakeCurrentUser.Id.Returns(userId);

            var orderId = Guid.NewGuid();
            var expectedOrder = new Order(orderId) { CreatorId = userId };

            var orders = new List<Order>() { expectedOrder }.AsQueryable();

            _fakeOrderRepository.WithDetailsAsync().Returns(orders);
            _fakeOrderRepository.CountAsync().Returns(1);
            _fakeAsyncExecuter.ToListAsync(Arg.Any<IQueryable<Order>>())
                .Returns(callInfo =>
                {
                    var query = callInfo.Arg<IQueryable<Order>>();
                    var result = query.ToList();
                    return Task.FromResult(result);
                });

            var ordersDto = new List<OrderDto>() { new OrderDto() { Id = orderId } };
            _fakeObjectMapper.Map<List<Order>, List<OrderDto>>(Arg.Any<List<Order>>())
                .Returns(ordersDto);

            // Act
            var ordersList = await _ordersAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Assert
            await _fakeOrderRepository.Received(1).WithDetailsAsync();
            ordersList.TotalCount.ShouldBe(1);
            ordersList.Items.ShouldBe(ordersDto);
        }

        [Fact]
        public async Task Should_Get_List_Async_His_Orders_If_Customer()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _fakeCurrentUser.IsInRole(MiniECommerceConsts.CustomerRole).Returns(true);
            _fakeCurrentUser.Id.Returns(userId);

            var orderId = Guid.NewGuid();
            var expectedOrder = new Order(orderId) { CreatorId = userId };

            var orders = new List<Order>() { expectedOrder }.AsQueryable();

            _fakeOrderRepository.WithDetailsAsync().Returns(orders);
            _fakeOrderRepository.CountAsync(Arg.Any<Expression<Func<Order, bool>>>()).Returns(1);
            _fakeAsyncExecuter.ToListAsync(Arg.Any<IQueryable<Order>>())
                .Returns(callInfo =>
                {
                    var query = callInfo.Arg<IQueryable<Order>>();
                    var result = query.ToList();
                    return Task.FromResult(result);
                });

            var ordersDto = new List<OrderDto>() { new OrderDto() { Id = orderId } };
            _fakeObjectMapper.Map<List<Order>, List<OrderDto>>(Arg.Any<List<Order>>())
                .Returns(ordersDto);

            // Act
            var ordersList = await _ordersAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Assert
            await _fakeOrderRepository.Received(1).WithDetailsAsync();
            ordersList.TotalCount.ShouldBe(1);
            ordersList.Items.ShouldBe(ordersDto);
        }

        [Fact]
        public async Task Should_Create_Async()
        {
            // Arrange
            var createUpdateOrderDto = new CreateUpdateOrderDto()
            {
                Items = new List<CreateUpdateOrderItemDto>()
                {
                    new CreateUpdateOrderItemDto() { ProductId = 1, Quantity = 2 },
                    new CreateUpdateOrderItemDto() { ProductId = 2, Quantity = 3 }
                }
            };
            _fakeProductRepository.GetListAsync(Arg.Any<Expression<Func<Product, bool>>>())
                .Returns(Task.FromResult(new List<Product>()
                {
                    new Product(1) { StockQuantity = 10 },
                    new Product(2) { StockQuantity = 5 }
                }));

            var order = new Order(Guid.NewGuid());
            _fakeObjectMapper.Map<CreateUpdateOrderDto, Order>(Arg.Any<CreateUpdateOrderDto>())
                .Returns(order);

            _fakeOrderRepository.InsertAsync(Arg.Any<Order>())
                .Returns(order);

            _fakeDistributedEventBus.PublishAsync(Arg.Any<IEnumerable<StockCountChangedEvent>>()).Returns(Task.CompletedTask);

            // Act
            var orderId = await _ordersAppService.CreateAsync(createUpdateOrderDto);

            // Assert
            await _fakeProductRepository.Received(1).GetListAsync(Arg.Any<Expression<Func<Product, bool>>>());
            await _fakeOrderRepository.Received(1).InsertAsync(Arg.Any<Order>());
            await _fakeDistributedEventBus.Received(1).PublishAsync(Arg.Any<IEnumerable<StockCountChangedEvent>>());
            orderId.ShouldBe(order.Id);
        }

        [Fact]
        public async Task Should_Throw_Exception_If_Order_Quantity_Exceeds_Stock()
        {
            // Arrange
            var createUpdateOrderDto = new CreateUpdateOrderDto()
            {
                Items = new List<CreateUpdateOrderItemDto>()
                {
                    new CreateUpdateOrderItemDto() { ProductId = 1, Quantity = 20 },
                    new CreateUpdateOrderItemDto() { ProductId = 2, Quantity = 3 }
                }
            };
            _fakeProductRepository.GetListAsync(Arg.Any<Expression<Func<Product, bool>>>())
                .Returns(Task.FromResult(new List<Product>()
                {
                    new Product(1) { StockQuantity = 10 },
                    new Product(2) { StockQuantity = 5 }
                }));

            // Act & Assert
            await Assert.ThrowsAsync<OrderQuantityException>(async () => await _ordersAppService.CreateAsync(createUpdateOrderDto));
            await _fakeProductRepository.Received(1).GetListAsync(Arg.Any<Expression<Func<Product, bool>>>());
        }
    }
}