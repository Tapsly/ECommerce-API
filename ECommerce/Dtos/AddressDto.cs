namespace ECommerce.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; } //Primary Key
        public string? Street { get; set; }
        public string? City { get; set; }
        public int CustomerId { get; set; } //Fore
    }
}
