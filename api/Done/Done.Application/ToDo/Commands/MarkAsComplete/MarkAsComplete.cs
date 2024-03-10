using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Commands.MarkAsComplete;

public sealed record MarkAsCompleteCommand(Guid ToDoId, bool isComplete = true) : ICommand<ToDo>;

internal sealed class MarkAsCompleteCommandHandler(DoneDbContext context) : ICommandHandler<MarkAsCompleteCommand, ToDo>
{
    public async Task<ToDo> Handle(MarkAsCompleteCommand request, CancellationToken cancellationToken)
    {
        var todo = await context.ToDos.FindAsync(new object[] { request.ToDoId }, cancellationToken);
        if (todo is null)
        {
            throw new ArgumentException("ToDo not found");
        }

        todo.IsDone = request.isComplete;

        await context.SaveChangesAsync(cancellationToken);

        return todo;
    }
}