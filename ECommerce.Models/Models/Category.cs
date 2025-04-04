using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; } // Primary Key
        [Required(ErrorMessage = "Name is required"),
            MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Ignore]
        public List<Product>? Products { get; set; }
    }
}
