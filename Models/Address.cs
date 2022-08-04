
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace customer_data_webAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? Zipcode { get; set; }

        public int Cus_address { get; set; }

        [JsonIgnore]
        public Customer? Customer_address { get; set; }


    }
}