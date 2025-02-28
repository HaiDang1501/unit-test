using Domain.Followers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Persistence.Configurations;

public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
{
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
        builder.HasIndex(x => new { x.UserId, x.FollowerId });

        builder.Property(x => x.CreatedUtc);
    }
}