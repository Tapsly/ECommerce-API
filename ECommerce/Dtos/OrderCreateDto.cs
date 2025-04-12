using ECommerce.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class OrderCreateDto
    {

        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ShippingAddressId { get; set; }
        [Required]
        public List<OrderItem> Items { get; set; } = null!;
    }
}
