using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class OrderItemCreateDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
