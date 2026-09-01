using TaskTracker.Core.Interfaces.Db;

namespace TaskTracker.Core.Entities;

public class TaskItem : IEntity
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
