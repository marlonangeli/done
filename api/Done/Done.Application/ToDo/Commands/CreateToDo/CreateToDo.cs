using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;

namespace Done.Application.Commands.CreateToDo;

public sealed record CreateToDoCommand(
        string Title,
        string Description,
        Priority Priority,
        Guid ToDoListId,
        DateTime? Due = null)
    : ICommand<ToDo>;

internal sealed class CreateToDoCommandHandler(DoneDbContext context) : ICommandHandler<CreateToDoCommand, ToDo>
{
    public async Task<ToDo> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        var toDo = new ToDo
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Due = request.Due,
            ToDoListId = request.ToDoListId
        };

        context.ToDos.Add(toDo);
        await context.SaveChangesAsync(cancellationToken);

        return toDo;
    }
}