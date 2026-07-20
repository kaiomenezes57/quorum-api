using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quorum.Domain.Entities;

namespace Quorum.Infrastructure.Persistence.Configurations;

public class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(128);
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(128);
        
        builder.Navigation(p => p.Options)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasOne(p => p.User)
            .WithMany(u => u.Polls)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.Options)
            .WithOne(o => o.Poll)
            .HasForeignKey(o => o.PollId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}