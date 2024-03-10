using Done.Application.Commands.CreateToDoList;
using Done.Application.Commands.MarkToDoListAsComplete;
using Done.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Done.Api.Endpoints;

public static class ToDoListEndpoints
{
    public static void MapToDoListEndpoints(this RouteGroupBuilder route)
    {
        route.MapGet("/", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllToDoListsQuery());

            return Results.Ok(result);
        });

        route.MapGet("{id:guid}", async (
            [FromServices] IMediator mediator,
            [FromRoute] Guid id) =>
        {
            var result = await mediator.Send(new GetToDoListByIdQuery(id));

            return Results.Ok(result);
        });

        route.MapGet("by-user/{id:guid}", async (
            [FromServices] IMediator mediator,
            [FromRoute] Guid id) =>
        {
            var result = await mediator.Send(new GetAllToDoListsByUserQuery(id));

            return Results.Ok(result);
        });

        route.MapPost("/", async (
            [FromServices] IMediator mediator,
            [FromBody] CreateToDoListCommand command) =>
        {
            var result = await mediator.Send(command);

            return Results.Created($"/{result.Id}", result);
        });

        route.MapPut("{id:guid}", async (
            [FromServices] IMediator mediator,
            [FromRoute] Guid id,
            [FromQuery] bool complete = true) =>
        {
            var result = await mediator.Send(new MarkToDoListAsCompleteCommand(id, complete));

            return Results.Ok(result);
        });
    }
}