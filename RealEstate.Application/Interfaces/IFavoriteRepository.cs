using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetByUserIdAsync(int userId);
        Task<Favorite> AddAsync(Favorite favorite);
        Task RemoveAsync(int userId, int propertyId);
    }
}
