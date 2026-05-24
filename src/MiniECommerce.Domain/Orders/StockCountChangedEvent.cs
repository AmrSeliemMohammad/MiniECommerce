using System;

namespace MiniECommerce.Orders
{
    public class StockCountChangedEvent
    {
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
    }

}
