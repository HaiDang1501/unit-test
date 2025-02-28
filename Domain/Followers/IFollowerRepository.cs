namespace Domain.Followers;

public interface IFollowerRepository
{
    public Task<bool> IsAlreadyFollowingAsync(Guid userId, Guid followerId, CancellationToken cancellationToken = default);

    public Task<bool> Insert(Follower follower, CancellationToken cancellationToken = default);
}
