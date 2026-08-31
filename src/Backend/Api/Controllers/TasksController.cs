using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskTracker.Api.Models;
using TaskTracker.Core.Queries.GetTask;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public partial class TasksController(
        IMediator mediator,
        ILogger<TasksController> logger
) : ControllerBase
{
    private const string GetTaskRouteName = $"{nameof(TasksController)}.{nameof(GetTask)}";

    [HttpGet("{id}", Name = GetTaskRouteName)]
    public async Task<IActionResult> GetTask(int id, CancellationToken cancelToken)
    {
        LogGetTaskById(id);

        var result = await mediator.Send(new GetTaskQuery(id), cancelToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(
        [FromBody] AddTaskModel model,
        CancellationToken cancelToken)
    {
        LogAddTask(model);

        var result = await mediator.Send(model.ToCommand(), cancelToken);

        var taskUri = result.IsSuccess
            ? Url.Link(GetTaskRouteName, new { id = result.Response!.Id })
            : null;

        return Created(taskUri, result);
    }
}
