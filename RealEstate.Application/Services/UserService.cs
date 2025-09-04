using Application.Common.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services.Security;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(IUserRepository userRepo, IMapper mapper, IPasswordHasher<User> passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ServiceResponse<string>> RegisterAsync(RegisterUserDto dto)
        {
            var response = new ServiceResponse<string>();

            if (await _userRepo.GetByEmailAsync(dto.Email) is not null)
            {
                response.Success = false;
                response.Message = "Email address is already registered.";
                return response;
            }

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _userRepo.CreateAsync(user);

            response.Success = true;
            response.Message = "User registered successfully.";
            response.Data = user.Id.ToString();

            return response;
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginUserDto dto)
        {
            var response = new ServiceResponse<string>();

            // Find user by email
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid email or password.";
                return response;
            }

            // Verify password
            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (passwordVerification == PasswordVerificationResult.Failed)
            {
                response.Success = false;
                response.Message = "Invalid email or password.";
                return response;
            }

            // Generate JWT
            var token = _jwtTokenGenerator.GenerateToken(user);
            response.Success = true;
            response.Message = "Login successful.";
            response.Data = token;

            return response;
        }


        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
