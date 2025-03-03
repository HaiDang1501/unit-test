using Microsoft.AspNetCore.Mvc;
using WebUI.Services;

namespace WebUI.Controllers;

public static class FollowerEndpoints
{
    public static void MapFollowerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("Follower/{userId:guid}/{followerId:guid}",
            async ([FromRoute] Guid userId, [FromRoute] Guid followerId, FollowerService followerservice) =>
            {
                var result = await followerservice.StartFollowingAsync(
                    userId,
                    followerId,
                    DateTime.UtcNow,
                    CancellationToken.None);

                return Results.Ok(result);
            });
    }
}

