using Done.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Commands.CreateToDoList;

public sealed class CreateToDoListCommandValidator : AbstractValidator<CreateToDoListCommand>
{
    public CreateToDoListCommandValidator(DoneDbContext context)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .MustAsync(async (id, cancellationToken) =>
            {
                var user = await context.Users.AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);
                return user;
            })
            .WithMessage("Usuário não encontrado");
    }
}