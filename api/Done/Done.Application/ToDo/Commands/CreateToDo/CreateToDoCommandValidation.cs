using Done.Domain.Entities;
using Done.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Done.Application.Commands.CreateToDo;

public sealed class CreateToDoCommandValidation : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidation(DoneDbContext context)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(Constants.ToDo.TitleMaxLength);

        RuleFor(x => x.Description)
            .MaximumLength(Constants.ToDo.DescriptionMaxLength);

        RuleFor(x => x.ToDoListId)
            .MustAsync(async (id, cancellationToken) =>
            {
                var toDoList = await context.ToDoLists.AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);
                return toDoList;
            })
            .WithMessage("Lista de tarefas não encontrada");
    }
}