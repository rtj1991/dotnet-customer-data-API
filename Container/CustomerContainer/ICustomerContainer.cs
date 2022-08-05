using customer_data_webAPI.Models;

public interface ICustomerContainer{
    Task<bool>Edit(CustomerDto customerDto);
    Task<List<Customer>> GetAll();

    Task<List<Customer>>GetCustomerByZip(int zipcode);
    Task<List<Customer>> SearchCustomer(string search);
    Task<double> GetDistance(int id, double longitude, double latitude);
}