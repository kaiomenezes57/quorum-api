using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Infrastructure.Persistence.Repositories
{
    public sealed class OptionRepository(AppDbContext context) : IOptionRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddAsync(Option option)
        {
            await _context.AddAsync(option);
        }

        public async Task<Option?> GetByIdAsync(Guid id)
        {
            return await _context.Options.FindAsync(id);
        }
    }
}
