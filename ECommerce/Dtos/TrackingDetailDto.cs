namespace ECommerce.Dtos
{
    public class TrackingDetailDto
    {
        public int Id { get; set; } //Primary Key
        public int OrderId { get; set; } //Foreign Key
        public string TrackingNumber { get; set; } = null!;
        public string Carrier { get; set; } = null!;
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
