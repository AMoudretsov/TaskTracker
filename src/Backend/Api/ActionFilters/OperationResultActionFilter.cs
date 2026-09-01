using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TaskTracker.Core.Models;

namespace TaskTracker.Api.ActionFilters;

public class OperationResultActionFilter(ProblemDetailsFactory problemDetailsFactory) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not null)
        {
            return;
        }

        if (context.Result is not ObjectResult)
        {
            return;
        }

        var objectResult = (ObjectResult)context.Result;
        if (objectResult.Value is not IOperationResult)
        {
            return;
        }

        var operationResult = (IOperationResult)objectResult.Value;
        if (operationResult.IsSuccess)
        {
            objectResult.Value = operationResult.Value;
            return;
        }

        context.Result = new ObjectResult(
            CreateProblemDetails(context, operationResult, problemDetailsFactory));
    }

    private static ProblemDetails CreateProblemDetails(
        ActionExecutedContext context,
        IOperationResult operationResult,
        ProblemDetailsFactory problemDetailsFactory)
    {
        // First error defines Status and Details props of the returned ProblemDetails
        var firstError = operationResult.Errors!.First();
        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            httpContext: context.HttpContext,
            statusCode: GetStatus(firstError),
            detail: firstError.Message);

        // Other error messages are added to the "errors" extension
        var otherErrors = operationResult.Errors!.Skip(1);
        if (otherErrors.Any())
        {
            problemDetails.Extensions["errors"] = otherErrors
                .Select(static er => er.Message)
                .ToArray();
        }

        return problemDetails;
    }

    private static int GetStatus(Error error)
    {
        return error.Code switch
        {
            ErrorCodes.InvalidInput => StatusCodes.Status400BadRequest,
            ErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
            ErrorCodes.NotFound => StatusCodes.Status404NotFound,
            ErrorCodes.InternalError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
