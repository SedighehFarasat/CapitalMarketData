using CapitalMarketData.BackgroundTask;
using CapitalMarketData.BackgroundTask.Services;
using CapitalMarketData.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<CapitalMarketDataDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        services.AddSingleton<FileLogger>();
        services.AddTransient<TseService>();
        services.AddHostedService<Worker>();
    })
    .Build();
await host.RunAsync();