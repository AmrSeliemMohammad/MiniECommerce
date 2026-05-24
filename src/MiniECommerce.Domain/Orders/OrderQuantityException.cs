using Volo.Abp;

namespace MiniECommerce.Orders
{
    public class OrderQuantityException : BusinessException
    {
        public OrderQuantityException() : base(MiniECommerceDomainErrorCodes.INVALID_ORDER_QUANTITY, "Order quantity must not exceed available stock")
        {
        }
    }
}
