using Domain.Abstractions;

namespace Domain.Followers;

public static class FollowerError
{
    public readonly static Error Sameuser = new(
        "Follower.SameUser",
        "Can't follow yourself");

    public readonly static Error AlreadyFollowing = new(
        "Follower.AlreadyFollowing",
        "Can't follow yourself");

    public readonly static Error NonePublicProfile = new(
        "Follower.NonPublicProfile",
        "Can't follow yourself");
}