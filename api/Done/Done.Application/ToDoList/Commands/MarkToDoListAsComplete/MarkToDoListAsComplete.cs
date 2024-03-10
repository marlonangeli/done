using Done.Application.Common.Abstraction;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Commands.MarkToDoListAsComplete;

public sealed record MarkToDoListAsCompleteCommand(Guid ToDoListId, bool IsComplete = true) : ICommand<int>;

internal sealed class MarkToDoListAsCompleteCommandHandler(DoneDbContext context) : ICommandHandler<MarkToDoListAsCompleteCommand, int>
{
    public async Task<int> Handle(MarkToDoListAsCompleteCommand request, CancellationToken cancellationToken)
    {
        var rows = await context.ToDos
            .Where(t => t.ToDoListId == request.ToDoListId)
            .ExecuteUpdateAsync(update =>
                update.SetProperty(todo => todo.IsDone, request.IsComplete), cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return rows;
    }
}