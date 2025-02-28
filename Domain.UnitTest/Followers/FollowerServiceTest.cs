using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;

namespace Domain.UnitTest.Followers;

public sealed class FollowerServiceTest
{
    private const string Email = "abc@gmail.com";
    private const string Email2 = "abc@gmail.com";
    private const string Name = "Hai Dang";
    private readonly DateTime Utc = DateTime.UtcNow;
    private readonly FollowerService _followerServiceMock;
    private readonly IFollowerRepository _followerRepositoryMock;

    public FollowerServiceTest()
    {
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();
        _followerServiceMock = new(_followerRepositoryMock);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        // Arrange
        var user = User.Create(Name,Email,true);

        // Act
        var result = await _followerServiceMock.StartFollowingAsync(user, user, Utc, default);

        // Assert

        result.Error.Should().Be(FollowerError.Sameuser);

    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingNonPublicProfile()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, false);

        // Act
        var result = await _followerServiceMock.StartFollowingAsync(user, followed, Utc, default);

        // Assert

        result.Error.Should().Be(FollowerError.NonePublicProfile);

    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenUserAlreadyFollowing()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, true);

        // Act
        _followerRepositoryMock.IsAlreadyFollowingAsync(user.Id, followed.Id, default).Returns(true);

        var result = await _followerServiceMock.StartFollowingAsync(user, followed, Utc, default);

        // Assert

        result.Error.Should().Be(FollowerError.AlreadyFollowing);

    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnSuccess_WhenFollowingCreated()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, true);

        // Act
        _followerRepositoryMock.IsAlreadyFollowingAsync(user.Id, followed.Id, default).Returns(false);

        var result = await _followerServiceMock.StartFollowingAsync(user, followed, Utc, default);

        // Assert

        result.IsSuccess.Should().BeTrue();

    }

    [Fact]
    public async Task StartFollowingAsync_Should_CallInsertMethod_WhenFollowingCreated()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, true);

        // Act
        _followerRepositoryMock.IsAlreadyFollowingAsync(user.Id, followed.Id, default).Returns(false);

        await _followerServiceMock.StartFollowingAsync(user, followed, Utc, default);

        // Assert

        _followerRepositoryMock.Received(1).Insert(Arg.Is<Follower>(x => x.UserId == user.Id && x.FollowerId == followed.Id));

    }
}
