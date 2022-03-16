using Cocona;
using Cocona.Filters;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ProductFetcher.Core.UseCases;
using ProductFetcher.Infrastructure.Extensions;

var builder = CoconaApp.CreateBuilder();
builder.Host.ConfigureAppConfiguration(b => b.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddCommandLine(args));

builder.Services.AddScoped<NotifyCustomerAboutPromotionsUseCase>();
builder.Services.AddNotifierInfrastructure(builder.Configuration);
builder.Configuration.AddJsonFile("appsettings.custom.json", optional: true);

var app = builder.Build();

app.UseFilter(new LoggingFilter(app.Services.GetRequiredService<ILogger<LoggingFilter>>()));
app.AddCommand(async (NotifyCustomerAboutPromotionsUseCase useCase) => {
     await useCase.Execute().ConfigureAwait(false);
});

await app.RunAsync().ConfigureAwait(false);

class LoggingFilter : CommandFilterAttribute
{
    private readonly ILogger _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }
    public override async ValueTask<int> OnCommandExecutionAsync(CoconaCommandExecutingContext ctx, CommandExecutionDelegate next)
    {
        _logger.LogInformation("Before: {Name}", ctx.Command.Name);
        try
        {
            return await next(ctx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {Name}", ctx.Command.Name);
            throw;
        }
        finally
        {
            _logger.LogInformation("End: {Name}", ctx.Command.Name);
        }
    }
}
