using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Services;

public class UserRepository(AuthDbContext context) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
    }
}