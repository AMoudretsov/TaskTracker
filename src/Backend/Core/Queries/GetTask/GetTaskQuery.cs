using MediatR;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Queries.GetTask;

public record GetTaskQuery(int Id) : IRequest<OperationResult<GetTaskResponse?>>;
