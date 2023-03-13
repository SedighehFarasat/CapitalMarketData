using CapitalMarketData.BackgroundTasks;
using CapitalMarketData.BackgroundTasks.Services;
using CapitalMarketData.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Logs/CapitalMarketData.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

var appName = $"{Assembly.GetExecutingAssembly().GetName().Name} ({Assembly.GetExecutingAssembly().GetName().Version})";
Log.Information($"{appName} Starting Up ...");

try
{
    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<CapitalMarketDataDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        services.AddHostedService<Worker>();
    })
    .Build();
    await host.RunAsync();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Error($"{appName} Caught Unhandled Exception: {ex.Message}");
}
finally
{
    Log.Information($"{appName} Shut Down Completely.");
    Log.CloseAndFlush();
}