using Microsoft.EntityFrameworkCore;
using Quorum.Domain.Entities;

namespace Quorum.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Poll> Polls { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}