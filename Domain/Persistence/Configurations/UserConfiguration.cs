using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.HasPublicProfile)
            .HasDefaultValue(false);

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}