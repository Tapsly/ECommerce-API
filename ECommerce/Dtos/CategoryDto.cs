using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
