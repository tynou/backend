using Auth.Domain.Entities;

namespace Auth.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    
    Task<User?> GetByUsernameAsync(string username);
    
    Task<User?> GetByEmailAsync(string email);
    
    Task SetVerifiedAsync(User user, bool isVerified);
}