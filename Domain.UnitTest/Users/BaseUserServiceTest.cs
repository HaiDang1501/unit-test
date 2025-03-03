using Domain.Users;
using NSubstitute;

namespace Domain.UnitTest.Users;

public abstract class BaseUserServiceTest
{
    protected readonly IUserRepository _userRepository;

    public BaseUserServiceTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
    }
}
