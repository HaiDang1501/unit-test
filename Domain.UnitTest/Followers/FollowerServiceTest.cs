using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using WebUI.Services;

namespace Domain.UnitTest.Followers;

public sealed class FollowerServiceTest
{
    private const string Email = "abc@gmail.com";
    private const string Email2 = "abc@gmail.com";
    private const string Name = "Hai Dang";
    private readonly DateTime Utc = DateTime.UtcNow;
    private readonly FollowerService _followerServiceMock;
    private readonly IFollowerRepository _followerRepositoryMock;
    private readonly IUserRepository _userRepositoryMock;

    public FollowerServiceTest()
    {
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _followerServiceMock = new(_followerRepositoryMock, _userRepositoryMock);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        // Arrange
        var user = User.Create(Name,Email,true);

        _userRepositoryMock.GetById(user.Id).Returns(user);

        // Act
        var result = await _followerServiceMock.StartFollowingAsync(user.Id, user.Id, Utc, default);

        // Assert

        result.Error.Should().Be(FollowerError.Sameuser);

    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingNonPublicProfile()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, false);
       
        _userRepositoryMock.GetById(user.Id).Returns(user);
        _userRepositoryMock.GetById(followed.Id).Returns(followed);

        // Act
        var result = await _followerServiceMock.StartFollowingAsync(user.Id, followed.Id, Utc, default);

        // Assert

        result.Error.Should().Be(FollowerError.NonePublicProfile);

    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenUserAlreadyFollowing()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, true);

        _userRepositoryMock.GetById(user.Id).Returns(user);
        _userRepositoryMock.GetById(followed.Id).Returns(followed);

        // Act
        _followerRepositoryMock.IsAlreadyFollowingAsync(user.Id, followed.Id, default).Returns(true);

        var result = await _followerServiceMock.StartFollowingAsync(user.Id, followed.Id, Utc, default);

        // Assert

        result.Error.Should().Be(FollowerError.AlreadyFollowing);

    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnSuccess_WhenFollowingCreated()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, true);

        _userRepositoryMock.GetById(user.Id).Returns(user);
        _userRepositoryMock.GetById(followed.Id).Returns(followed);

        // Act
        _followerRepositoryMock.IsAlreadyFollowingAsync(user.Id, followed.Id, default).Returns(false);

        var result = await _followerServiceMock.StartFollowingAsync(user.Id, followed.Id, Utc, default);

        // Assert

        result.IsSuccess.Should().BeTrue();

    }

    [Fact]
    public async Task StartFollowingAsync_Should_CallInsertMethod_WhenFollowingCreated()
    {
        // Arrange
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email2, true);

        _userRepositoryMock.GetById(user.Id).Returns(user);
        _userRepositoryMock.GetById(followed.Id).Returns(followed);

        // Act
        _followerRepositoryMock.IsAlreadyFollowingAsync(user.Id, followed.Id, default).Returns(false);

        await _followerServiceMock.StartFollowingAsync(user.Id, followed.Id, Utc, default);

        // Assert

        await _followerRepositoryMock.Received(1).Insert(Arg.Is<Follower>(x => x.UserId == user.Id && x.FollowerId == followed.Id));

    }
}
