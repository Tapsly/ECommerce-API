using ECommerce.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
    }
}
