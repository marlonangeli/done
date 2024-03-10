using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Queries;

public sealed record GetAllUsersQuery : IQuery<List<User>>;

internal sealed class GetAllUsersQueryHandler(DoneDbContext context) : IQueryHandler<GetAllUsersQuery, List<User>>
{
    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) =>
        await context.Users.AsNoTracking().OrderByDescending(x => x.UpdatedAt).ToListAsync(cancellationToken);
}