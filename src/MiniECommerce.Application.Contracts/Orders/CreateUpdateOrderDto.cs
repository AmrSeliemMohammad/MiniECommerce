using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniECommerce.Orders
{
    public class CreateUpdateOrderDto
    {
        [Required]
        public List<CreateUpdateOrderItemDto> Items { get; set; }
    }
}
