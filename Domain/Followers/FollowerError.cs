using Domain.Abstractions;

namespace Domain.Followers;

public static class FollowerError
{
    public readonly static Error Sameuser = new Error(
        "Follower.SameUser",
        "Can't follow yourself");

    public readonly static Error AlreadyFollowing = new Error(
        "Follower.AlreadFollowing",
        "Can't follow yourself");

    public readonly static Error NonePublicProfile = new Error(
        "Follower.NonPublicProfile",
        "Can't follow yourself");
}
