namespace TaskTracker.Core.Interfaces.Validation;

public interface IValidationResult
{
    bool IsValid { get; }

    IEnumerable<IValidationFailure> Errors { get; }

    IReadOnlyDictionary<string, string[]> ErrorMessagesGroupedByProperty { get; }
}
