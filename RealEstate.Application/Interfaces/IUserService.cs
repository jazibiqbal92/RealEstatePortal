using Application.Common.Responses;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> RegisterAsync(RegisterUserDto dto);
        Task<ServiceResponse<string>> LoginAsync(LoginUserDto dto);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
