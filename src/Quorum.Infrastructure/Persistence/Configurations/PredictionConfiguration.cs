using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quorum.Domain.Entities;
using Quorum.Domain.Enums;

namespace Quorum.Infrastructure.Persistence.Configurations;

public class PredictionConfiguration : IEntityTypeConfiguration<Prediction>
{
    public void Configure(EntityTypeBuilder<Prediction> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Result)
            .HasConversion<string>();
        
        builder.HasOne(p => p.User)
            .WithMany(u => u.Predictions)
            .HasForeignKey(p => p.UserId);

        builder.HasOne(p => p.Poll)
            .WithMany()
            .HasForeignKey(p => p.PollId);

        builder.HasOne(p => p.Option)
            .WithMany(o => o.Predictions)
            .HasForeignKey(p => p.OptionId);
    }
}