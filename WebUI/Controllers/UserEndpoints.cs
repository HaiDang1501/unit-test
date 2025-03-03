using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using WebUI.Services;

namespace WebUI.Controllers;

public static class UserEndpoints
{
    private const string UserRoute = "User";
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(UserRoute);

        group.MapPost(
            "",
            async ([FromBody] CreateUserRequest body, UserService service) =>
            {
                var result = await service.Create(
                    body,
                    CancellationToken.None);

                return Results.Ok(result);
            });

        group.MapGet("/{id:guid}",
            async ([FromRoute] Guid id, UserService service) =>
            {
                var result = await service.GetById(
                    id,
                    CancellationToken.None);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

        group.MapGet("",
            async (UserService service) =>
            {
                var result = await service.GetAll(CancellationToken.None);

                return Results.Ok(result);
            });
    }
}

