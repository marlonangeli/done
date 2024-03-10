using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetAllToDoListsQuery : IQuery<List<ToDoList>>;

internal sealed class GetAllToDoListsQueryHandler(DoneDbContext context) : IQueryHandler<GetAllToDoListsQuery, List<ToDoList>>
{
    public async Task<List<ToDoList>> Handle(GetAllToDoListsQuery request, CancellationToken cancellationToken) =>
        await context.ToDoLists
            .AsNoTracking()
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
}