using Quorum.Domain.Entities;

namespace Quorum.Domain.Repositories;

public interface IPollRepository
{
    Task CreateAsync(Poll poll);
    
    Task<IReadOnlyList<Poll>> GetAllAsync();
    Task<Poll?> GetByIdAsync(Guid id);
}