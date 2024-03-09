using Done.Application.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Done.Api.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/api/users/", async (
            [FromBody] CreateUserCommand command,
            [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            return Results.Ok(result);
        }).WithOpenApi();

        return app;
    }
}