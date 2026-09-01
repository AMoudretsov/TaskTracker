using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionResults;

namespace TaskTracker.Api.Controllers;

[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    public virtual NoContentObjectResult NoContent(object? value)
    {
        return new NoContentObjectResult(value);
    }
}
