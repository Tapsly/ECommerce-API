using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class CustomerLoginDto
    {
        [Required(ErrorMessage = "Invalid Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
