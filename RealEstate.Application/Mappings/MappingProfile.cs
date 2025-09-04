using AutoMapper;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstate.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Property
            CreateMap<Property, PropertyCreateDto>().ReverseMap();
            CreateMap<Property, PropertyDto>()
            .ForMember(dest => dest.ListingType, opt => opt.MapFrom(src => src.ListingType.ToString()));

            // Favourites
            CreateMap<Favorite, FavoriteCreateDto>().ReverseMap();
            CreateMap<Favorite, FavoriteDto>()
      .ForMember(dest => dest.UserEmail,
                 opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty))
      .ForMember(dest => dest.PropertyTitle,
                 opt => opt.MapFrom(src => src.Property != null ? src.Property.Title : string.Empty))
      .ForMember(dest => dest.PropertyPrice,
                 opt => opt.MapFrom(src => src.Property != null ? src.Property.Price : (decimal?)null))
      .ForMember(dest => dest.PropertyListingType,
                 opt => opt.MapFrom(src => src.Property != null ? src.Property.ListingType.ToString() : string.Empty))
      .ForMember(dest => dest.PropertyImageUrl,
                 opt => opt.MapFrom(src => src.Property != null ? src.Property.ImageUrl : string.Empty));


        }
    }
}
