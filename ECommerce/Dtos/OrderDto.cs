namespace ECommerce.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public string CustomerPhoneNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime ShippedDate { get; set; }
    }
}
