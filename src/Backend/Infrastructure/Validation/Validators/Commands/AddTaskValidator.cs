using FluentValidation;
using TaskTracker.Core.Commands.AddTask;

namespace TaskTracker.Infrastructure.Validation.Validators.Commands;

public class AddTaskValidator : AbstractValidator<AddTaskCommand>
{
    public AddTaskValidator()
    {
        RuleFor(cmd => cmd.Title)
            .NotEmpty().WithMessage("Task title must be not empty string")
            .MaximumLength(128).WithMessage("Task title length must be less than or equal to {MaxLength}");

        RuleFor(cmd => cmd.Description)
            .MaximumLength(4096).WithMessage("Task description length must be less than or equal to {MaxLength}")
            .When(cmd => cmd.Description is not null);
    }
}
