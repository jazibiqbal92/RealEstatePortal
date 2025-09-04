using Application.Common.Responses;
using AutoMapper;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repo;
        private readonly IMapper _mapper;

        public FavoriteService(IFavoriteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FavoriteDto>> GetByUserIdAsync(int userId)
        {
            var favorites = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<FavoriteDto>>(favorites);
        }

        public async Task<ServiceResponse<FavoriteDto>> AddAsync(FavoriteCreateDto dto)
        {
            var response = new ServiceResponse<FavoriteDto>();

            // Duplicate check for pair
            var existingFavorites = await _repo.GetByUserIdAsync(dto.UserId);
            if (existingFavorites.Any(f => f.PropertyId == dto.PropertyId))
            {
                response.Success = false;
                response.Message = "This property is already marked as favorite.";
                return response;
            }

            var favorite = _mapper.Map<Favorite>(dto);
            var created = await _repo.AddAsync(favorite);

            response.Data = _mapper.Map<FavoriteDto>(created);
            response.Success = true;
            response.Message = "Property added to favorites successfully.";

            return response;
        }


        public async Task RemoveAsync(int userId, int propertyId)
        {
            await _repo.RemoveAsync(userId, propertyId);
        }
    }
}
