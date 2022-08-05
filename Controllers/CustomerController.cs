using customer_data_webAPI.Models;
using JsonNet.PrivateSettersContractResolvers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace customer_data_webAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IServiceProvider serviceProvider;
    private readonly ICustomerContainer customerContainer;

    private readonly IDistanceCalculationContainer distanceCalculation;
    public CustomerController(IServiceProvider _serviceProvider, ICustomerContainer _customerContainer, IDistanceCalculationContainer _distanceCalculation)
    {
        this.serviceProvider = _serviceProvider;
        this.customerContainer = _customerContainer;
        this.distanceCalculation = _distanceCalculation;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet(Name = "SaveCustomer")]
    public void SaveCustomer()
    {

        var jsontext = System.IO.File.ReadAllText(@"/home/thara/Documents/ASPDOTNET_2/customer-data-webAPI/UserData.json");

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        };

        List<Customer> customer =
         JsonConvert.DeserializeObject<List<Customer>>(
           jsontext, settings);

        using (
            var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope
                          .ServiceProvider.GetService<CustomerDBContext>();
            if (!context.Customers.Any())
            {
                context.AddRange(customer);
                context.SaveChanges();
            }

        }

    }

    [HttpPost("Edit")]
    public async Task<IActionResult> Edit(CustomerDto customerDto)
    {
        var my_trip = await this.customerContainer.Edit(customerDto);
        return Ok(true);
    }

    // [Authorize(Roles = "ADMIN")]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var places = await this.customerContainer.GetAll();

        return Ok(places);
    }

    [HttpGet("GetCustomerByZip/{zipcode}")]
    public async Task<IActionResult> GetCustomerByZip(int zipcode)
    {
        var customer = await this.customerContainer.GetCustomerByZip(zipcode);

        return Ok(customer);
    }

    [HttpGet("SearchCustomer/{search}")]
    public async Task<IActionResult> SearchCustomer(string search)
    {
        var customer = await this.customerContainer.SearchCustomer(search);

        return Ok(customer);
    }

    [HttpGet("GetDistance/{id}/{longitude}/{latitude}")]
    public async Task<IActionResult> GetDistance(int id, double longitude, double latitude)
    {
        var customer = await this.distanceCalculation.GetDistance(id, longitude, latitude);

        return Ok(customer);
    }
}

