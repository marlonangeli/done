using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;

namespace Done.Application.Commands.CreateToDoList;

public sealed record CreateToDoList(string Title, Guid UserId) : ICommand<ToDoList>;

internal sealed class CreateToDoListCommandHandler(DoneDbContext context) : ICommandHandler<CreateToDoList, ToDoList>
{
    public async Task<ToDoList> Handle(CreateToDoList request, CancellationToken cancellationToken)
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