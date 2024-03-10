using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetAllToDosQuery : IQuery<List<ToDo>>;

internal  sealed class GetAllToDosQueryHandler(DoneDbContext context) : IQueryHandler<GetAllToDosQuery, List<ToDo>>
{
    public async Task<List<ToDo>> Handle(GetAllToDosQuery request, CancellationToken cancellationToken) =>
        await context.ToDos
            .AsNoTracking()
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
}