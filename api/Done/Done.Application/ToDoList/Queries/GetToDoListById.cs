using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetToDoListByIdQuery(Guid Id) : IQuery<ToDoList>;

internal sealed class GetToDoListByIdQueryHandler(DoneDbContext context) : IQueryHandler<GetToDoListByIdQuery, ToDoList>
{
    public async Task<ToDoList> Handle(GetToDoListByIdQuery request, CancellationToken cancellationToken) =>
        (await context.ToDoLists
            .AsNoTracking()
            .AsSplitQuery()
            .Include(i => i.ToDos)
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken))!;
}