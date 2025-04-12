using ECommerce.Models.Models;

namespace ECommerce.Dtos
{
    public class OrderDetailsDto
    {
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public string CustomerPhoneNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string ShippedDate { get; set; } = null!;
        public AddressDetailsDto ShippingAddress { get; set; } = null!;
        public List<OrderItemDto> OrderItems { get; set; } = null!;
        public TrackingDetail TrackingDetail { get; set; } = null!;
    }
}
