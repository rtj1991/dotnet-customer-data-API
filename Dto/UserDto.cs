namespace customer_data_webAPI.Models
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Password { get; set; }
        public string Role { get; set; } = null!;

    }
}
