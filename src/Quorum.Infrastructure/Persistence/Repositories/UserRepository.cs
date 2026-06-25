using Microsoft.EntityFrameworkCore;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Infrastructure.Persistence.Repositories
{
    public sealed class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Polls)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => string.Equals(email, x.Email));
        }
    }
}