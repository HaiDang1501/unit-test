using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using WebUI.Services;

namespace WebUI.Controllers;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "user",
            async ([FromBody] CreateUserRequest body, UserService service) =>
            {
                var result = await service.Create(
                    body,
                    CancellationToken.None);

                return Results.Ok(result);
            });
    }
}

