using customer_data_webAPI.Models;

public interface IUserContainer{
    Task<bool>Save(User userDto);
}