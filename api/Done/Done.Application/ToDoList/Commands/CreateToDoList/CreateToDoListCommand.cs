using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;

namespace Done.Application.Commands.CreateToDoList;

public sealed record CreateToDoListCommand(string Title, Guid UserId) : ICommand<ToDoList>;

internal sealed class CreateToDoListCommandHandler(DoneDbContext context) : ICommandHandler<CreateToDoListCommand, ToDoList>
{
    public async Task<ToDoList> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
    {
        var toDoList = new ToDoList
        {
            Title = request.Title,
            UserId = request.UserId
        };

        context.ToDoLists.Add(toDoList);
        await context.SaveChangesAsync(cancellationToken);

        return toDoList;
    }
}