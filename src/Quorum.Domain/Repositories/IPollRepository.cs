using Quorum.Domain.Entities;

namespace Quorum.Domain.Repositories;

public interface IPollRepository
{
    Task<Guid> CreateAsync(Poll poll);
    Task<IReadOnlyList<Poll>> GetAllAsync();
}