using RealEstate.Application.DTOs;

namespace RealEstate.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetAllAsync(decimal? minPrice, decimal? maxPrice, int? bedrooms, int? bathrooms);
        Task<PropertyDto> GetByIdAsync(int id);
        Task<PropertyDto> CreateAsync(PropertyCreateDto dto);
        Task<PropertyDto> UpdateAsync(int id, PropertyCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
