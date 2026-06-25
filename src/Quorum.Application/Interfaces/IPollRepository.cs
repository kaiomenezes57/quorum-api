using Quorum.Domain.Entities;

namespace Quorum.Application.Interfaces
{
    public interface IPollRepository
    {
        Task AddAsync(Poll poll);
        Task<Poll?> GetByIdAsync(Guid id);
    }
}
