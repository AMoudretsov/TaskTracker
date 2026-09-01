using Microsoft.Extensions.Logging;
using MediatR;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Messaging.PipelineBehaviors;

public partial class CatchAllPipelineBehavior<TRequest, TOperationResult>(
    ILogger<CatchAllPipelineBehavior<TRequest, TOperationResult>> logger
) : IPipelineBehavior<TRequest, TOperationResult>
    where TRequest : notnull
    where TOperationResult : IFailureFactory<TOperationResult>
{
    public async Task<TOperationResult> Handle(
        TRequest request,
        RequestHandlerDelegate<TOperationResult> next,
        CancellationToken cancelToken)
    {
        try
        {
            return await next(cancelToken);
        }
        catch (Exception ex)
        {
            LogUnhandledException(ex);

            return TOperationResult.Failure(
                [new Error(ErrorCodes.InternalError, "Error occurred while performing the operation")]);
        }
    }
}
