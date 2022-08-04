using AutoMapper;
using customer_data_webAPI.Models;

public class AutoMapperHandler : Profile
{
    public AutoMapperHandler()
    {
        CreateMap<Customer, CustomerDto>().ForMember(Customer => Customer._Id, opt => opt.MapFrom(item => item._Id));

    }
}