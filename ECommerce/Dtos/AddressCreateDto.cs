using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class AddressCreateDto
    {
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "City is required"),
            MinLength(2, ErrorMessage = "City must not be less than 2 Characters"),
                MaxLength(50, ErrorMessage = "City must be less than 50 Characters")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "ZipCode is required"),
            MinLength(4, ErrorMessage = "ZipCode must not be less than 4 digits"),
                MaxLength(6, ErrorMessage = "ZipCode must be less than 6 digits")]
        public string ZipCode { get; set; } = null!;
    }
}
