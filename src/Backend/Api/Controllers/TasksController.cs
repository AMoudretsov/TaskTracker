using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public partial class TasksController(ILogger<TasksController> logger) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        LogGetTaskById(id);

        var dummyTask = new
        {
            Id = 11,
            Title = "Solve task #11",
            Description = "Task details",
            CreatedAt = DateTime.UtcNow
        };

        return Ok(dummyTask);
    }
}
