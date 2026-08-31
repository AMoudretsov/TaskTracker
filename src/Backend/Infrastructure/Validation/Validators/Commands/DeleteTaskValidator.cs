using FluentValidation;
using TaskTracker.Core.Commands.DeleteTask;

namespace TaskTracker.Infrastructure.Validation.Validators.Commands;

public class DeleteTaskValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskValidator()
    {
        RuleFor(cmd => cmd.Id)
            .GreaterThan(0).WithMessage("Task id must be positive integer");
    }
}
