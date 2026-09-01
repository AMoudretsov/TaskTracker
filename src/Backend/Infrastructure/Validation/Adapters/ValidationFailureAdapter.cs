using FluentValidation.Results;
using TaskTracker.Core.Interfaces.Validation;

namespace TaskTracker.Infrastructure.Validation.Adapters;

public class ValidationFailureAdapter : IValidationFailure
{
    public ValidationFailureAdapter(ValidationFailure failure)
    {
        ArgumentNullException.ThrowIfNull(failure);

        PropertyName = failure.PropertyName;
        ErrorMessage = failure.ErrorMessage;
    }

    public string PropertyName { get; }

    public string ErrorMessage { get; }
}
