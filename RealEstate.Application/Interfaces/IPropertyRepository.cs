using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync(decimal? minPrice, decimal? maxPrice, int? bedrooms, int? bathrooms);
        Task<Property?> GetByIdAsync(int id);
        Task<Property> CreateAsync(Property property);
        Task<Property?> UpdateAsync(Property property);
        Task<bool> DeleteAsync(Property property);
    }
}
