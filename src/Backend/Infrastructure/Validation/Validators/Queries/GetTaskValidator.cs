using FluentValidation;
using TaskTracker.Core.Queries.GetTask;

namespace TaskTracker.Infrastructure.Validation.Validators.Queries;

public class GetTaskValidator : AbstractValidator<GetTaskQuery>
{
    public GetTaskValidator()
    {
        RuleFor(qry => qry.Id)
            .GreaterThan(0).WithMessage("Task id must be positive integer");
    }
}
