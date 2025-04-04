using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class TrackingDetailCreateDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Courier is required")]
        public string Carrier { get; set; } = null!;
        [Required(ErrorMessage = "Estimated Delivery date is required")]
        public DateTime EstimatedDeliveryDate { get; set; }
        [Required(ErrorMessage = "Tracking Number is required"),
            MaxLength(500, ErrorMessage = "Tracking Number must be less than 500 characters")]
        public string TrackingNumber { get; set; } = null!;
    }
}
