using Quorum.Domain.Entities;

namespace Quorum.Domain.Repositories
{
    public interface IPollRepository
    {
        Task AddAsync(Poll poll);
        Task<Poll?> GetByIdAsync(Guid id);
    }
}
