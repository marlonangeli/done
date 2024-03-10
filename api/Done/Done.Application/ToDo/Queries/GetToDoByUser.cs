using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetToDoByUserQuery(Guid UserId) : IQuery<List<ToDo>>;

public sealed class GetToDoByUserQueryHandler(DoneDbContext context) : IQueryHandler<GetToDoByUserQuery, List<ToDo>>
{
    public async Task<List<ToDo>> Handle(GetToDoByUserQuery request, CancellationToken cancellationToken) =>
        await context.ToDos
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(o => o.UpdatedAt)
            .Include(i => i.ToDoList)
            .Where(todo => todo.ToDoList.UserId == request.UserId)
            .Select(s => s.IgnoreToDoList())
            .ToListAsync(cancellationToken);
}

internal static class ToDoQueryExtensions
{
    public static ToDo IgnoreToDoList(this ToDo toDo)
    {
        toDo.ToDoList = null;
        return toDo;
    }
}