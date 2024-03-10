using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetToDoByToDoListQuery(Guid ToDoListId) : IQuery<List<ToDo>>;

internal sealed class GetToDoByToDoListQueryHandler(DoneDbContext context) : IQueryHandler<GetToDoByToDoListQuery, List<ToDo>>
{
    public async Task<List<ToDo>> Handle(GetToDoByToDoListQuery request, CancellationToken cancellationToken)
    {
        return await context.ToDos
            .AsNoTracking()
            .Where(todo => todo.ToDoListId == request.ToDoListId)
            .OrderByDescending(o => o.UpdatedAt)
            .ToListAsync(cancellationToken);
    }
}