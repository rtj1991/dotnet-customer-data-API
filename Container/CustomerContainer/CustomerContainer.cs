using AutoMapper;
using customer_data_webAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerContainer : ICustomerContainer
{
    private readonly CustomerDBContext _DBContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerContainer> _logger;

    public CustomerContainer(CustomerDBContext _DBContext, IMapper _mapper, ILogger<CustomerContainer> logger)
    {
        this._DBContext = _DBContext;
        this._mapper = _mapper;
        this._logger = logger;
    }
    public async Task<bool> Edit(CustomerDto customerDto)
    {

        try
        {
            _logger.LogInformation("Edit Customer Data !");
            if (customerDto._Id != null)
            {
                var _csutomer = await _DBContext.Customers.Include(m => m.Address).Where(c => c._Id == customerDto._Id).SingleOrDefaultAsync();
                if (_csutomer != null)
                {

                    if (customerDto.Name != null)
                    {
                        _csutomer.Name = customerDto.Name;
                    }
                    else
                    {
                        _csutomer.Name = _csutomer.Name;
                    }

                    if (customerDto.Email != null)
                    {
                        _csutomer.Email = customerDto.Email;
                    }
                    else
                    {
                        _csutomer.Email = _csutomer.Email;
                    }

                    if (customerDto.Phone != null)
                    {
                        _csutomer.Phone = customerDto.Phone;
                    }
                    else
                    {
                        _csutomer.Phone = _csutomer.Phone;
                    }

                    this._DBContext.Update(_csutomer);
                    await this._DBContext.SaveChangesAsync();

                    return true;

                }
                else
                {
                    throw new NullReferenceException("Getting Null while fetching Customer details");
                }

            }
            else
            {
                _logger.LogInformation("Edit Customer Data  Null Argument Detected");
                throw new ArgumentNullException("Getting Error while Argument Pass !");
            }

        }
        catch (ArgumentNullException e)
        {

            throw new ArgumentNullException("Getting Error while Argument Pass ! " + e.Message);
        }

    }

    public Task<List<Customer>> GetAll()
    {
        _logger.LogInformation("Fetching All The Customer Details");
        List<Customer> resp = new List<Customer>();

        var _customer = _DBContext.Customers.Include(b => b.Address).ToList();
        if (_customer != null)
        {
            resp = _mapper.Map<List<Customer>, List<Customer>>(_customer);
        }
        else
        {
            _logger.LogInformation("Getting Null while fetching Customer details");
            throw new NullReferenceException("Getting Null while fetching Customer details");
        }
        return Task.FromResult(resp);
    }

    public async Task<List<Customer>> GetCustomerByZip(int zipcode)
    {
        try
        {
            _logger.LogInformation("Fetching Customer details By Zipcode");
            var customer = await _DBContext.Customers.Include(b => b.Address).Where(b => b.Address.Zipcode == zipcode).ToListAsync();

            if (customer != null)
            {
                List<Customer> resp = _mapper.Map<List<Customer>, List<Customer>>(customer);
                return resp;
            }
            else
            {
                _logger.LogInformation("Getting Null while fetching Customer details");
                throw new NullReferenceException("Getting Null while fetching Customer details");
            }
        }
        catch (ArgumentNullException e)
        {
            _logger.LogInformation("Getting Error while Argument Pass " + e.Message);
            throw new ArgumentNullException("Getting Error while Argument Pass " + e.Message);
        }
    }



    public async Task<List<Customer>> SearchCustomer(string search)
    {
        try
        {
            _logger.LogInformation("Fetching Customer details By Any Text");

            var customer = await _DBContext.Customers.Include(b => b.Address).Where(b => b.Name.Contains(search) || b._Id.Contains(search) || b.EyeColor.Contains(search) || b.Gender.Contains(search) || b.Company.Contains(search) || b.Email.Contains(search) || b.Phone.Contains(search) || b.About.Contains(search) || b.Registered.Contains(search) || b.Address.Street.Contains(search) || b.Address.City.Contains(search) || b.Address.State.Contains(search)).ToListAsync();

            if (customer != null)
            {
                List<Customer> resp = _mapper.Map<List<Customer>, List<Customer>>(customer);
                return resp;
            }
            else
            {
                _logger.LogInformation("Getting Null while fetching Customer detailse");
                throw new NullReferenceException("Getting Null while fetching Customer details");
            }
        }
        catch (ArgumentNullException e)
        {
            _logger.LogInformation("Getting Error while Argument Pass "+e.Message);
            throw new ArgumentNullException("Getting Error while Argument Pass " + e.Message);
        }
    }

}