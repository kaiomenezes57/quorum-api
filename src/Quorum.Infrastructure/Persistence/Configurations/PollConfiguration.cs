using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quorum.Domain.Entities;

namespace Quorum.Infrastructure.Persistence.Configurations
{
    public sealed class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(x => x.Owner)
                .WithMany(x => x.Polls)
                .HasForeignKey(x => x.OwnerId);
        }
    }
}
