using CapitalMarketData.BackgroundTasks;
using CapitalMarketData.BackgroundTasks.Services;
using CapitalMarketData.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");

var appName = $"{Assembly.GetExecutingAssembly().GetName().Name} ({Assembly.GetExecutingAssembly().GetName().Version})";
logger.LogInformation($"{appName} Starting Up ...");

try
{
    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<CapitalMarketDataDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        services.AddSingleton<FileLogger>();
        services.AddHostedService<Worker>();
    })
    .Build();
    await host.RunAsync();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    logger.LogCritical($"{appName} Caught Unhandled Exception: {ex.Message}");
}
finally
{
    logger.LogInformation($"{appName} Shut Down Completely.");
}