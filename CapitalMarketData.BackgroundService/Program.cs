using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CapitalMarketData.Persistence;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<CapitalMarketDataDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value);
        });
    })
    .Build();
await host.RunAsync();