using AutoMapper;
using MiniECommerce.Orders;

namespace MiniECommerce.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() 
        { 
            CreateMap<Order, OrderDto>();
            CreateMap<CreateUpdateOrderDto, Order>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<CreateUpdateOrderItemDto, OrderItem>();
        }
    }
}
