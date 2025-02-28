using System.Reflection.Metadata.Ecma335;
using Domain.Users;

namespace WebUI.Services;

public class UserService(IUserRepository repository)
{
    private readonly IUserRepository _repository = repository;

    public async Task<bool> Create(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var newUser = request.ToDomain();

        var result = await _repository.Insert(
            newUser,
            cancellationToken);

        return result;
    }

    public async Task<GetUserByIdResponse?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(
            id,
            cancellationToken);

        return result is not null ? GetUserByIdResponse.FromDomain(result) : null;
    }
}

public record CreateUserRequest(string Name, string Email)
{
    public User ToDomain()
    {
        return User.Create(
            Name,
            Email,
            true);
    }
};

public record CreateUserResponse(Guid Id, string Name, string Email, bool HasDefaultProfile);

public record GetUserByIdResponse(Guid Id, string Name, string Email, bool HasDefaultProfile)
{
    public static GetUserByIdResponse FromDomain(User user)
    {
        return new(
            user.Id,
            user.Name,
            user.Email,
            user.HasPublicProfile);
    }
}
