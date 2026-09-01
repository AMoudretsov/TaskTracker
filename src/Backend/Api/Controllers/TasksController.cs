using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskTracker.Api.Models;
using TaskTracker.Core.Commands.DeleteTask;
using TaskTracker.Core.Models;
using TaskTracker.Core.Queries.GetTask;
using TaskTracker.Core.Queries.ListTasks;

namespace TaskTracker.Api.Controllers;

[ApiController]
public partial class TasksController(
        IMediator mediator,
        ILogger<TasksController> logger
) : ApiController
{
    private const string GetTaskRouteName = $"{nameof(TasksController)}.{nameof(GetTask)}";

    [HttpGet]
    public async Task<IActionResult> ListTasks(
        [FromQuery] PagingQuery paging,
        CancellationToken cancelToken)
    {
        LogGetTasks(paging);

        var result = await mediator.Send(new ListTasksQuery(paging), cancelToken);

        return Ok(result);
    }

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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(
        int id,
        [FromBody] UpdateTaskModel model,
        CancellationToken cancelToken)
    {
        LogUpdateTask(model);

        var result = await mediator.Send(model.ToCommand(id), cancelToken);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id, CancellationToken cancelToken)
    {
        LogDeleteTask(id);

        var result = await mediator.Send(new DeleteTaskCommand(id), cancelToken);

        return NoContent(result);
    }
}
