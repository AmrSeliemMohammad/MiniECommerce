using System.ComponentModel.DataAnnotations;

namespace MiniECommerce.Orders
{
    public class CreateUpdateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
