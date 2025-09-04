using Application.Common.Responses;
using RealEstate.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteDto>> GetByUserIdAsync(int userId);
        Task<ServiceResponse<FavoriteDto>> AddAsync(FavoriteCreateDto dto);
        Task RemoveAsync(int userId, int propertyId);
    }
}
