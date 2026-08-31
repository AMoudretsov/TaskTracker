using FluentValidation.Results;
using TaskTracker.Core.Interfaces.Validation;

namespace TaskTracker.Infrastructure.Validation.Adapters;

public class ValidationResultAdapter : IValidationResult
{
    public ValidationResultAdapter(ValidationResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        IsValid = result.IsValid;

        Errors = result.Errors
            .Select(f => new ValidationFailureAdapter(f))
            .ToArray();

        ErrorMessagesGroupedByProperty = result
            .ToDictionary()
            .AsReadOnly();
    }

    public bool IsValid { get; }

    public IEnumerable<IValidationFailure> Errors { get; }

    public IReadOnlyDictionary<string, string[]> ErrorMessagesGroupedByProperty { get; }
}
