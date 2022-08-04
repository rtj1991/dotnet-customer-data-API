using AutoMapper;
using customer_data_webAPI.Models;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

public class CustomerContainer : ICustomerContainer
{
    private readonly CustomerDBContext _DBContext;
    private readonly IMapper _mapper;

    public CustomerContainer(CustomerDBContext _DBContext,IMapper _mapper)
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
}