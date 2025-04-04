using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Product Name is require")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Product Category is required")]
        public int CategoryId { get; set; }
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be a negative value")]
        public int Stock { get; set; }
    }
}
