namespace Domain.Followers;

public sealed class Follower
{
    public Guid UserId { get; private set; }
    public Guid FollowerId { get; private set; }
    public DateTime CreatedUtc { get; private set; }

    private Follower()
    {
        
    }

    private Follower(Guid userId, Guid followerId, DateTime createdUtc)
    {
        UserId = userId;
        FollowerId = followerId;
        CreatedUtc = createdUtc;
    }

    public static Follower Create(
        Guid userId, 
        Guid followerId, 
        DateTime createdUtc)
    {
        return new(
            userId,
            followerId,
            createdUtc);
    }
}
