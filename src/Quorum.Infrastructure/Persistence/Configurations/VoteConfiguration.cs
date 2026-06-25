using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quorum.Domain.Entities;

namespace Quorum.Infrastructure.Persistence.Configurations
{
    public sealed class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Voter)
                .WithMany()
                .HasForeignKey(x => x.VoterId);

            builder.HasOne(x => x.Poll)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.PollId);

            builder.HasOne(x => x.Option)
                .WithMany()
                .HasForeignKey(x => x.OptionId);

            builder.Property(x => x.VoteDate)
                .IsRequired();
        }
    }
}