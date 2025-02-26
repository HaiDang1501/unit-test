namespace Domain.Followers;

public interface IFollowerRepository
{
    public Task<bool> IsAlreadyFollowingAsync(Guid userId, Guid followerId, CancellationToken cancellationToken = default);

    public Follower Insert(Follower follower, CancellationToken cancellationToken = default);
}
