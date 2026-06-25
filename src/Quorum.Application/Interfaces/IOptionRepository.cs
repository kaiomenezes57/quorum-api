using Quorum.Domain.Entities;

namespace Quorum.Application.Interfaces
{
    public interface IOptionRepository
    {
        Task AddAsync(Option option);
        Task<Option?> GetByIdAsync(Guid id);
    }
}
