using customer_data_webAPI.Models;

public interface ICustomerContainer{
    Task<bool>Edit(CustomerDto customerDto);
    Task<List<Customer>> GetAll();
}