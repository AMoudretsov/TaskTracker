using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskTracker.Core.Queries.GetTask;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public partial class TasksController(
        IMediator mediator,
        ILogger<TasksController> logger
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id, CancellationToken cancelToken)
    {
        LogGetTaskById(id);

        var result = await mediator.Send(new GetTaskQuery(id), cancelToken);

        return Ok(result);
    }
}
