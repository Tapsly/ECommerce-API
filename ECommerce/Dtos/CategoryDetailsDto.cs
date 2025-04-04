using ECommerce.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class CategoryDetailsDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public List<Product>? Products { get; set; }
    }
}
