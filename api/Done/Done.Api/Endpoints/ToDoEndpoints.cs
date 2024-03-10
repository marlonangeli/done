using System.Runtime.InteropServices;
using Done.Application.Commands.CreateToDo;
using Done.Application.Commands.MarkAsComplete;
using Done.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Done.Api.Endpoints;

public static class ToDoEndpoints
{
    public static void MapToDoEndpoints(this RouteGroupBuilder route)
    {
        route.MapGet("/", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllToDosQuery());

            return Results.Ok(result);
        });

        route.MapGet("/by-user/{id:guid}", async (
            [FromServices] IMediator mediator,
            [FromRoute] Guid id) =>
        {
            var result = await mediator.Send(new GetToDoByUserQuery(id));

            return Results.Ok(result);
        });

        route.MapGet("/by-list/{id:guid}", async (
            [FromServices] IMediator mediator,
            [FromRoute] Guid id) =>
        {
            var result = await mediator.Send(new GetToDoByToDoListQuery(id));

            return Results.Ok(result);
        });

        route.MapPost("/", async (
            [FromServices] IMediator mediator,
            [FromBody] CreateToDoCommand command) =>
        {
            var result = await mediator.Send(command);

            return Results.Created($"/{result.Id}", result);
        });

        route.MapPut("{id:guid}", async (
            [FromServices] IMediator mediator,
            [FromRoute] Guid id,
            [FromQuery] bool complete = true) =>
        {
            var result = await mediator.Send(new MarkAsCompleteCommand(id, complete));

            return Results.Ok(result);
        });
    }
}