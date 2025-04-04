using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Models
{
    public class Address
    {
        public int Id { get; set; } //Primary Key
        [Required(ErrorMessage = "Street is required"),
            MinLength(5),
                MaxLength(200, ErrorMessage = "Street must not be longer than 200 characters")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "City is required"),
            MinLength(2),
                MaxLength(100, ErrorMessage = "City must not be longer than 100 characters")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "Zip Code is required"),
            MinLength(4, ErrorMessage = "Zip Code cannot be less than 4 digits!"),
                MaxLength(10, ErrorMessage = "Zip Code must not be longer than 10 digits")]
        public string ZipCode { get; set; } = null!;
        public int CustomerId { get; set; } //Foreign Key
        [ForeignKey("CustomerId")]
        [Ignore]
        public Customer Customer { get; set; } = null!; // Navigation property for the Customer
    }
}
