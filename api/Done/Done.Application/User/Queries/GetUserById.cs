using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<User>;

internal sealed class GetUserByIdQueryHandler(DoneDbContext context) : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        (await context.Users
            .AsNoTracking()
            .AsSplitQuery()
            .Include(i => i.ToDoLists)
            .ThenInclude(ti => ti.ToDos)
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken))!;
}