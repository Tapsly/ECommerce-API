using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class CustomerCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be atleast 3 characters long ")]
        [MaxLength(20, ErrorMessage = "Name must not be longer than 20 characters")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "LastName is required")]
        [MinLength(3, ErrorMessage = "LastName must be atleast 3 characters long ")]
        [MaxLength(20, ErrorMessage = "LastName must not be longer than 20 characters")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "PhoneNumber is required")]
        [MinLength(3, ErrorMessage = "PhoneNumber must be atleast 11 characters long ")]
        [MaxLength(20, ErrorMessage = "Name must not be longer than 20 characters")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 char long")]
        public string Password { get; set; } = null!;
    }
}
