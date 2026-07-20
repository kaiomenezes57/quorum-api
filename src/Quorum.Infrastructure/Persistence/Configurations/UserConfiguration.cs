using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quorum.Domain.Entities;

namespace Quorum.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.OwnsOne(u => u.Password, p =>
        {
            p.Property(pwd => pwd.Hash)
                .HasColumnName("PasswordHash")
                .IsRequired();
        });

        builder.Navigation(u => u.Polls)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasMany(u => u.Votes)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Navigation(u => u.Votes)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}