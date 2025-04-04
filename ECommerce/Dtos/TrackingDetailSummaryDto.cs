using ECommerce.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class TrackingDetailSummaryDto
    {
        public Order Order { get; set; } = null!;
        [Required]
        public string Carrier { get; set; } = null!;
        public DateTime EstimatedDeliveryDate { get; set; }
        [MaxLength(500)]
        public string TrackingNumber { get; set; } = null!;
    }
}
