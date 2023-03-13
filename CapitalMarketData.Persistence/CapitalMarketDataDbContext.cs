using CapitalMarketData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CapitalMarketData.Persistence;
public class CapitalMarketDataDbContext : DbContext
{
    public CapitalMarketDataDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<TradingData> TradingData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}