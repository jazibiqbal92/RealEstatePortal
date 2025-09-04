using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly RealEstateContext _context;
        public PropertyRepository(RealEstateContext context) => _context = context;

        public async Task<IEnumerable<Property>> GetAllAsync(decimal? minPrice, decimal? maxPrice, int? bedrooms, int? bathrooms)
        {
            var query = _context.Properties.AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (bedrooms.HasValue)
                query = query.Where(p => p.BedroomCount == bedrooms.Value);

            if (bathrooms.HasValue)
                query = query.Where(p => p.BedroomCount == bathrooms.Value);

            return await query.ToListAsync();
        }


        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Property> CreateAsync(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Property?> UpdateAsync(Property property)
        {
            var existing = await _context.Properties.FindAsync(property.Id);
            if (existing == null)
                return null;

            _context.Entry(existing).CurrentValues.SetValues(property);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Property property)
        {
            if (property == null)
                return false;

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
