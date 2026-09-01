using TaskTracker.Core.Interfaces.Validation;

namespace TaskTracker.Infrastructure.Validation.Adapters;

public class ValidatorAdapter<T>(FluentValidation.IValidator<T> validator) : IValidator<T>
{
    public IValidationResult Validate(T entity)
    {
        return new ValidationResultAdapter(validator.Validate(entity));
    }
}
