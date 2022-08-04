using AutoMapper;
using customer_data_webAPI.Models;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

public class CustomerContainer : ICustomerContainer
{
    private readonly CustomerDBContext _DBContext;
    private readonly IMapper _mapper;

    public CustomerContainer(CustomerDBContext _DBContext, IMapper _mapper)
    {
        this._DBContext = _DBContext;
        this._mapper = _mapper;
    }
    public async Task<bool> Edit(CustomerDto customerDto)
    {

        try
        {
            if (customerDto._Id != null)
            {
                var _csutomer = await _DBContext.Customers.FindAsync(customerDto._Id);
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
        List<Customer> resp = new List<Customer>();

        var _customer = _DBContext.Customers.Include(b => b.Address).ToList();
        if (_customer != null)
        {
            resp = _mapper.Map<List<Customer>, List<Customer>>(_customer);
        }
        else
        {
            throw new NullReferenceException("Getting Null while fetching Customer details");
        }
        return Task.FromResult(resp);
    }

    public async Task<List<Customer>> GetCustomerByZip(int zipcode)
    {
        try
        {

            var customer = await _DBContext.Customers.Include(b => b.Address).Where(b => b.Address.Zipcode == zipcode).ToListAsync();

            if (customer != null)
            {
                List<Customer> resp = _mapper.Map<List<Customer>, List<Customer>>(customer);
                return resp;
            }
            else
            {
                throw new NullReferenceException("Getting Null while fetching Customer details");
            }
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentNullException("Getting Error while Argument Pass " + e.Message);
        }
    }



    public async Task<List<Customer>> SearchCustomer(string search)
    {
        try
        {

            var customer = await _DBContext.Customers.Include(b => b.Address).Where(b => b.Name.Contains(search) || b._Id.Contains(search) || b.EyeColor.Contains(search) || b.Gender.Contains(search) || b.Company.Contains(search) || b.Email.Contains(search) || b.Phone.Contains(search) || b.About.Contains(search) || b.Registered.Contains(search) || b.Address.Street.Contains(search) || b.Address.City.Contains(search) || b.Address.State.Contains(search)).ToListAsync();

            if (customer != null)
            {
                List<Customer> resp = _mapper.Map<List<Customer>, List<Customer>>(customer);
                return resp;
            }
            else
            {
                throw new NullReferenceException("Getting Null while fetching Customer details");
            }
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentNullException("Getting Error while Argument Pass " + e.Message);
        }
    }

    public async Task<double> GetDistance(int id, double longitude, double latitude)
    {

        var _csutomer = await _DBContext.Customers.FindAsync(id);

        double rlat1 = Math.PI * latitude / 180;
        double rlat2 = (double)(Math.PI * _csutomer.Latitude / 180);

        double theta = (double)(longitude - _csutomer.Longitude);

        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        return dist * 1.609344;
    }
}