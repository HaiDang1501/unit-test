using Domain.Abstractions;
using Domain.Followers;
using Domain.Users;

namespace WebUI.Services;

public sealed class FollowerService(
    IFollowerRepository followerRepository,
    IUserRepository userRepository)
{
    private readonly IFollowerRepository _followerRepository = followerRepository;
    private readonly IUserRepository _userRepository = userRepository;


    public async Task<Result> StartFollowingAsync(
        Guid userId,
        Guid followedId,
        DateTime utcNow,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(userId, cancellationToken);
        var followed = await _userRepository.GetById(followedId, cancellationToken);

        if (user!.Id == followed!.Id)
        {
            return Result.Failure(FollowerError.Sameuser);
        }

        if (!followed.HasPublicProfile)
        {
            return Result.Failure(FollowerError.NonePublicProfile);
        }

        if (await _followerRepository.IsAlreadyFollowingAsync(user.Id, followed.Id, cancellationToken))
        {
            return Result.Failure(FollowerError.AlreadyFollowing);
        }

        var follower = Follower.Create(
            user.Id,
            followed.Id,
            utcNow);

        await _followerRepository.Insert(follower, cancellationToken);

        return Result.Success(follower);
    }
}
