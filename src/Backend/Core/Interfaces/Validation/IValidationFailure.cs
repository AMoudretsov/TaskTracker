namespace TaskTracker.Core.Interfaces.Validation;

public interface IValidationFailure
{
    public string PropertyName { get; }

    public string ErrorMessage { get; }
}
