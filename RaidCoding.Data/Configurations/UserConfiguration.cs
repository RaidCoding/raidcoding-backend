using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaidCoding.Data.Models;

namespace RaidCoding.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.AvatarUrl)
            .HasMaxLength(255);

        builder.Property(u => u.Bio)
            .HasMaxLength(256);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("NOW()");
    }
}