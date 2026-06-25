using Quorum.Application.Interfaces;

namespace Quorum.Infrastructure.Persistence
{
    public sealed class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
