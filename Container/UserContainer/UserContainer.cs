using AutoMapper;
using customer_data_webAPI.Models;

public class UserContainer : IUserContainer
{
    private readonly CustomerDBContext _DBContext;
    private readonly IMapper _mapper;

    public UserContainer(CustomerDBContext _DBContext, IMapper _mapper)
    {
        this._DBContext = _DBContext;
        this._mapper = _mapper;
    }

    public async Task<bool> Save(User userDto)
    {
        _DBContext.Add(userDto);
        await this._DBContext.SaveChangesAsync();
        return true;
    }
}