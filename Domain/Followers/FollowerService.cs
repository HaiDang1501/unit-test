//using Domain.Abstractions;
//using Domain.Users;

//namespace Domain.Followers;

//public sealed class FollowerService
//{
//    private readonly IFollowerRepository _followerRepository;

//    public FollowerService(IFollowerRepository followerRepository)
//    {
//        _followerRepository = followerRepository;
//    }

//    public async Task<Result> StartFollowingAsync(
//        User user,
//        User followed,
//        DateTime utcNow,
//        CancellationToken cancellationToken)
//    {
//        if(user.Id == followed.Id)
//        {
//            return Result.Failure(FollowerError.Sameuser);
//        }

//        if (!followed.HasPublicProfile)
//        {
//            return Result.Failure(FollowerError.NonePublicProfile);
//        }

//        if (await _followerRepository.IsAlreadyFollowingAsync(user.Id, followed.Id, cancellationToken))
//        {
//            return Result.Failure(FollowerError.AlreadyFollowing);
//        }

//        var follower = Follower.Create(
//            user.Id,
//            followed.Id,
//            utcNow);

//        _followerRepository.Insert(follower, cancellationToken);

//        return Result.Success(follower);
//    }
//}
