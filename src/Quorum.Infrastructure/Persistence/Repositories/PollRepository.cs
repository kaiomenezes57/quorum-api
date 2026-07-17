using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Infrastructure.Persistence.Repositories;

public class PollRepository(AppDbContext context) : IPollRepository
{
    public async Task<Guid> CreateAsync(Poll poll)
    {
        await context.Polls.AddAsync(poll);
        return poll.Id;
    }

    public async Task<IReadOnlyList<Poll>> GetAllAsync()
    {
        var polls = context.Polls.ToList();
        return polls.AsReadOnly();
    }
}