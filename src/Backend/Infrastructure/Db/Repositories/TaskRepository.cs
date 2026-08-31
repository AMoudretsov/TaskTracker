using TaskTracker.Core.Entities;
using TaskTracker.Core.Interfaces.Db;

namespace TaskTracker.Infrastructure.Db.Repositories;

public class TaskRepository(TasksDbContext context) : Repository<TaskItem>(context), ITaskRepository;
