using Microsoft.EntityFrameworkCore;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Infrastructure.Persistence.Repositories
{
    public sealed class PollRepository(AppDbContext context) : IPollRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddAsync(Poll poll)
        {
            await _context.Polls.AddAsync(poll);
        }

        public async Task<Poll?> GetByIdAsync(Guid id)
        {
            var a = await _context.Polls
                .Include(x => x.Options)
                .Include(x => x.Votes)
                .FirstOrDefaultAsync(x => x.Id == id);
            return a;
        }
    }
}
