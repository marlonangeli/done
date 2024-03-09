using Done.Application.Common.Abstraction;
using Done.Domain.Entities;
using Done.Infrastructure.Context;

namespace Done.Application.Commands.CreateUser;

public sealed record CreateUserCommand(string Username, string Email) : ICommand<User>;

internal sealed class CreateUserCommandHandler(DoneDbContext context) : ICommandHandler<CreateUserCommand, User>
{
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return user;
    }
}