using FluentValidation;
using TaskTracker.Core.Queries.ListTasks;

namespace TaskTracker.Infrastructure.Validation.Validators.Queries;

public class ListTasksValidator : AbstractValidator<ListTasksQuery>
{
    public ListTasksValidator()
    {
        RuleFor(qry => qry.Search)
            .MaximumLength(128).WithMessage("Length of search term cannot exceed 128 characters")
            .When(qry => !string.IsNullOrEmpty(qry.Search));

        RuleFor(qry => qry.Paging.Limit)
            .GreaterThanOrEqualTo(1).WithMessage("Paging limit must be in the range [1, 1000]")
            .LessThanOrEqualTo(1000).WithMessage("Paging limit must be in the range [1, 1000]")
            .When(qry => qry.Paging.Limit.HasValue);

        RuleFor(qry => qry.Paging.Cursor)
            .GreaterThan(0).WithMessage("Paging cursor must be positive integer")
            .When(qry => qry.Paging.Cursor.HasValue);
    }
}
