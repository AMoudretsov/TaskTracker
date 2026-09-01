using TaskTracker.Api.Extensions;
using TaskTracker.Infrastructure.Db.Extensions;
using TaskTracker.Infrastructure.Logging.Extensions;
using TaskTracker.Infrastructure.Messaging.Extensions;
using TaskTracker.Infrastructure.Validation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddProblemDetails()
    .AddControllersAndGlobalFilters()
    .AddLowercaseUrls()
    .AddTasksDb(builder.Configuration)
    .AddInProcessMessaging()
    .AddLogging(builder.Configuration)
    .AddValidators();

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
