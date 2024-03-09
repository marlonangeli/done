using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries.GetToDos;

public sealed record GetToDosQuery : IQuery<List<ToDo>>;

internal  sealed class GetToDosQueryHandler(DoneDbContext context) : IQueryHandler<GetToDosQuery, List<ToDo>>
{
    public async Task<List<ToDo>> Handle(GetToDosQuery request, CancellationToken cancellationToken) =>
        await context.ToDos.AsNoTracking().OrderByDescending(x => x.UpdatedAt).ToListAsync(cancellationToken);
}