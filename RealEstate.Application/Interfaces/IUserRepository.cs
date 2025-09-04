﻿using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<User> CreateAsync(User user);
    }
}
