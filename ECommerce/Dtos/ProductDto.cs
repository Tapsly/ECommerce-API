using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; } // Primary Key
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPercentage { get; set; } //Product Level Discount
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(50)]
        public int CategoryId { get; set; } // Foreign Key
    }
}
