using AutoMapper;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Shared;
using RealEstate.Domain.Entities;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;

namespace RealEstate.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IPropertyNotifier _notifier;

        public PropertyService(IPropertyRepository repo, IMapper mapper, ICacheService cache, IPropertyNotifier notifier)
        {
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
            _notifier = notifier;
        }

        //public async Task<IEnumerable<PropertyDto>> GetAllAsync(decimal? minPrice, decimal? maxPrice,int? bedrooms,int? bathrooms)
        //{
        //    var props = await _repo.GetAllAsync(minPrice, maxPrice, bedrooms, bathrooms);
        //    return _mapper.Map<IEnumerable<PropertyDto>>(props);
        //}

        public async Task<IEnumerable<PropertyDto>> GetAllAsync(decimal? minPrice, decimal? maxPrice, int? bedrooms, int? bathrooms)
        {
            // Create a unique key based on filters
            string cacheKey = $"properties:{minPrice}:{maxPrice}:{bedrooms}:{bathrooms}";

            // Try to get from cache
            var cachedData = await _cache.GetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<PropertyDto>>(cachedData)!;
            }

            // Otherwise fetch from DB
            var props = await _repo.GetAllAsync(minPrice, maxPrice, bedrooms, bathrooms);
            var result = _mapper.Map<IEnumerable<PropertyDto>>(props);

            // Store result in cache (and track the key inside Redis set)
            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<PropertyDto> GetByIdAsync(int id)
        {
            var prop = await _repo.GetByIdAsync(id);
            return _mapper.Map<PropertyDto>(prop);
        }

        public async Task<PropertyDto> CreateAsync(PropertyCreateDto dto)
        {
            var prop = _mapper.Map<Property>(dto);
            var created = await _repo.CreateAsync(prop);

            //  Invalidate relevant cache
            await _cache.RemoveByPatternAsync("properties:keys");
            await _notifier.NotifyPropertyAdded(dto);

            return _mapper.Map<PropertyDto>(created);
        }

        public async Task<PropertyDto> UpdateAsync(int id, PropertyCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return null;

            _mapper.Map(dto, existing);
            dto.Id = id;
            var updated = await _repo.UpdateAsync(existing);

            return _mapper.Map<PropertyDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return false;

            return await _repo.DeleteAsync(existing);
        }

    }
}
