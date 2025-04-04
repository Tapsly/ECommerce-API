using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; } //Primary Key
        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = null!;
        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;
    }
}
