
using System.ComponentModel.DataAnnotations;

namespace customer_data_webAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? _Id { get; set; }
        public int? Index { get; set; }
        public int? Age { get; set; }
        public string? EyeColor { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? About { get; set; }
        public string? Registered { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public Address? Address { get; set; }

        public string[]? Tags { get; set; }
        
    }
}