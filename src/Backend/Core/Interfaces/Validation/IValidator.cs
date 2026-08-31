namespace TaskTracker.Core.Interfaces.Validation;

public interface IValidator<TEntity>
{
    IValidationResult Validate(TEntity entity);
}
