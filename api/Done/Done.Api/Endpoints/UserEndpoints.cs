using Done.Application.Commands.CreateUser;
using Done.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Done.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this RouteGroupBuilder route)
    {
        route.MapGet("/", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllUsersQuery());

            return Results.Ok(result);
        });

        route.MapGet("{id:guid}", async (
            [FromServices] IMediator mediator,
            [FromRoute] Guid id) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));

            return Results.Ok(result);
        });

        route.MapPost("/", async (
            [FromServices] IMediator mediator,
            [FromBody] CreateUserCommand command) =>
        {
            var result = await mediator.Send(command);

            return Results.Created($"/{result.Id}", result);
        });
    }
}