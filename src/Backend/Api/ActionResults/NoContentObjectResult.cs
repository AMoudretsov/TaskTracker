using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TaskTracker.Api.ActionResults;

[DefaultStatusCode(DefaultStatusCode)]
public class NoContentObjectResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status204NoContent;

    public NoContentObjectResult(object? value) : base(value)
    {
        StatusCode = DefaultStatusCode;
    }
}
