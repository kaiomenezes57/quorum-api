using Microsoft.EntityFrameworkCore;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email) 
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByUsernameAsync(string username) 
        => await context.Users.FirstOrDefaultAsync(u => u.Name == username);
}