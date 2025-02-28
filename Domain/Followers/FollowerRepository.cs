using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Domain.Followers;

public class FollowerRepository(ApplicationDbContext context) : IFollowerRepository
{
    private readonly ApplicationDbContext _dbContext = context;

    public async Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followerId,
        CancellationToken cancellationToken = default)
    {
        var getFollowed = await _dbContext
            .Follower
            .Where(x => x.UserId.Equals(userId) && x.FollowerId.Equals(followerId))
            .FirstOrDefaultAsync(cancellationToken);

        return getFollowed is not null;
    }

    public async Task<bool> Insert(Follower follower, CancellationToken cancellationToken = default)
    {
        var createNewFollower = _dbContext
            .Follower
            .Add(follower);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}