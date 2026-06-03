using MiniECommerce.Products;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MiniECommerce.Orders
{
    public class StockCountChangedHandler_Tests
    {
        [Fact]
        public async Task Should_Handle_Event()
        {
            //Arrange
            var product1 = new Product(1)
            {
                StockQuantity = 20
            };
            var product2 = new Product(2)
            {
                StockQuantity = 15
            };
            var productsList = new List<Product>(){ product1, product2 };

            var fakeProductRepository = Substitute.For<IRepository<Product, int>>();
            fakeProductRepository.GetListAsync(Arg.Any<Expression<Func<Product, bool>>>()).ReturnsForAnyArgs(productsList);

            var stockCountChangedHandler = new StockCountChangedHandler(fakeProductRepository);

            var stockCountChangedEventList = new List<StockCountChangedEvent>
            {
                new StockCountChangedEvent
                {
                    ProductId = 1,
                    OrderQuantity = 2
                },
                new StockCountChangedEvent
                {
                    ProductId = 2,
                    OrderQuantity = 1
                }
            };

            fakeProductRepository.UpdateManyAsync(Arg.Any<List<Product>>()).Returns(Task.CompletedTask);
            //Act
            await stockCountChangedHandler.HandleEventAsync(stockCountChangedEventList);

            //Assert
            await fakeProductRepository.Received(1).GetListAsync(Arg.Any<Expression<Func<Product, bool>>>());
            await fakeProductRepository.Received(1).UpdateManyAsync(Arg.Is<List<Product>>(list => list.Count == 2 && list.Contains(product1) && list.Contains(product2)));
        }
    }
}
