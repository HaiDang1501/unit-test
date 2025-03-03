using Domain.Users;
using NSubstitute;
using WebUI.Services;

namespace Domain.UnitTest.Users;

public class UserServiceCreateTest
{
    private readonly IUserRepository _userRepository;
    private readonly UserService _userService;
    public UserServiceCreateTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new(_userRepository);
    }

    [Fact]
    public async Task Create_Should_ReturnTrue_WhenInsertedUser()
    {
        // Arrange
        var request = new CreateUserRequest("Hai Dang", "abc@gmail.com");
        
        var user = request.ToDomain();

        // Act
        await _userService.Create(request, default);

        _userRepository.Insert(user, default).Returns(true);

        // Assert
        await _userRepository.Received(1).Insert(Arg.Is<User>(x =>  x.Name == "Hai Dang"), default);
    }
}
