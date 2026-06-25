using Quorum.Domain.Entities;

namespace Quorum.Domain.Repositories
{
    public interface IOptionRepository
    {
        Task AddAsync(Option option);
        Task<Option?> GetByIdAsync(Guid id);
    }
}
