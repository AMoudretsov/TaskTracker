using TaskTracker.Api.Extensions;
using TaskTracker.Infrastructure.Db.Extensions;
using TaskTracker.Infrastructure.Logging.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddProblemDetails()
    .AddLowercaseUrls()
    .AddControllers();

builder.Services
    .AddTasksDb(builder.Configuration)
    .AddLogging(builder.Configuration);

var webApp = builder.Build();

if (webApp.Environment.IsDevelopment())
{
    webApp.MapOpenApi();
}

webApp
    .UseExceptionHandler()
    .UseStatusCodePages()
    .UseRequestLogging()
    .UseHttpsRedirection()
    .UseAuthorization();

webApp.MapControllers();

webApp.Run();
