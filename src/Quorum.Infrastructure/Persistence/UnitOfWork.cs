using Quorum.Application.Interfaces;

namespace Quorum.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync() 
        => await context.SaveChangesAsync();
}