using Microsoft.Extensions.Logging;
using MediatR;
using TaskTracker.Core.Interfaces.Validation;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Messaging.PipelineBehaviors;

public partial class ValidationPipelineBehavior<TRequest, TOperationResult>(
    IValidator<TRequest> validator,
    ILogger<ValidationPipelineBehavior<TRequest, TOperationResult>> logger
) : IPipelineBehavior<TRequest, TOperationResult>
    where TRequest : notnull
    where TOperationResult : IFailureFactory<TOperationResult>
{
    public Task<TOperationResult> Handle(
        TRequest request,
        RequestHandlerDelegate<TOperationResult> next,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (validationResult.IsValid)
        {
            return next(cancellationToken);
        }

        LogValidationFailure(request.GetType().Name, validationResult.ErrorMessagesGroupedByProperty);

        var errors = validationResult.Errors
            .Select(er => new Error(ErrorCodes.InvalidInput, er.ErrorMessage))
            .ToArray();

        return Task.FromResult(TOperationResult.Failure(errors));
    }
}
