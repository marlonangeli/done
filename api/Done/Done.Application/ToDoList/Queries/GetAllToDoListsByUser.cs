using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetAllToDoListsByUserQuery(Guid UserId) : IQuery<List<ToDoList>>;

internal sealed class GetAllToDoListsByUserQueryHandler(DoneDbContext context) : IQueryHandler<GetAllToDoListsByUserQuery, List<ToDoList>>
{
    public async Task<List<ToDoList>> Handle(GetAllToDoListsByUserQuery request, CancellationToken cancellationToken) =>
        await context.ToDoLists
            .AsNoTracking()
            .AsSplitQuery()
            .Include(i => i.ToDos)
            .IgnoreAutoIncludes()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
}