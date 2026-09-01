using FluentValidation;
using TaskTracker.Core.Commands.UpdateTask;

namespace TaskTracker.Infrastructure.Validation.Validators.Commands;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(cmd => cmd.Id)
            .GreaterThan(0).WithMessage("Task id must be positive integer");

        RuleFor(cmd => cmd.Title)
            .NotEmpty().WithMessage("Task title must be not empty string")
            .MaximumLength(128).WithMessage("Task title length must be less than or equal to {MaxLength}");

        RuleFor(cmd => cmd.Description)
            .MaximumLength(4096).WithMessage("Task description length must be less than or equal to {MaxLength}")
            .When(cmd => cmd.Description is not null);

        RuleFor(cmd => cmd.IsCompleted)
            .NotNull().WithMessage("Missing value of task property {PropertyPath}");

        RuleFor(cmd => cmd.CreatedAt)
            .NotNull().WithMessage("Missing value of task property {PropertyPath}");
    }
}
