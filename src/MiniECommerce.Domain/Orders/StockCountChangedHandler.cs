using MiniECommerce.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace MiniECommerce.Orders
{
    public class StockCountChangedHandler :
        IDistributedEventHandler<IEnumerable<StockCountChangedEvent>>,
        ITransientDependency
    {
        private readonly IRepository<Product, int> _productRepository;
        public StockCountChangedHandler(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleEventAsync(IEnumerable<StockCountChangedEvent> eventData)
        {
           List<Product> productsList = _productRepository.GetListAsync(p => eventData.Select(i => i.ProductId).Contains(p.Id)).Result.ToList();
            
            productsList.ForEach(p =>
            {
                var item = eventData.First(i => i.ProductId == p.Id);
                p.StockQuantity -= item.OrderQuantity;      
            });

            await _productRepository.UpdateManyAsync(productsList);
        }
    }

}
