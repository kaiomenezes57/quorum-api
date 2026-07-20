using Microsoft.EntityFrameworkCore;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Infrastructure.Persistence.Repositories;

public class PollRepository(AppDbContext context) : IPollRepository
{
    public async Task CreateAsync(Poll poll)
    {
        await context.Polls.AddAsync(poll);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Poll>> GetAllAsync()
    {
        var polls = 
            await context.Polls 
                .Include(p => p.Options) 
                .AsNoTracking()
                .ToListAsync();
        
        return polls.AsReadOnly();
    }

    public Task<Poll?> GetByIdAsync(Guid id)
    {
        var poll = context.Polls
            .Include(p => p.Options)
            .FirstOrDefaultAsync(p => p.Id == id);

        return poll!;
    }
}