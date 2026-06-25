using Microsoft.EntityFrameworkCore;
using Quorum.Domain.Entities;

namespace Quorum.Infrastructure.Persistence
{
    public sealed class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; private set; }

        public DbSet<Poll> Polls { get; private set; }
        public DbSet<Option> Options { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}