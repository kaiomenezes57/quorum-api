using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quorum.Domain.Entities;

namespace Quorum.Infrastructure.Persistence.Configurations;

public class OptionCinfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .ValueGeneratedNever();
        
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasOne(o => o.Poll)
            .WithMany(p => p.Options)
            .HasForeignKey(o => o.PollId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Votes)
            .WithOne(o => o.Option)
            .HasForeignKey(o => o.OptionId);
        
        builder.Navigation(o => o.Votes)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}