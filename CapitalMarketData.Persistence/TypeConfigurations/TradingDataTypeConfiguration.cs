using CapitalMarketData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalMarketData.Persistence.TypeConfigurations;
public class TradingDataTypeConfiguration : IEntityTypeConfiguration<TradingData>
{
    private const string _tableName = "TradingData";

    public void Configure(EntityTypeBuilder<TradingData> builder)
    {
        builder.ToTable(_tableName);
        
        builder.HasKey(x => new { x.InstrumentIsin, x.Date });
        
        builder.Property(x => x.Date)
            .IsRequired();
        builder.Property(x => x.InstrumentIsin)
            .IsRequired()
            .HasMaxLength(16);
        builder.Property(x => x.Status);
        builder.Property(x => x.OpeningPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.HighestPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.LowestPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.LastPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.ClosingPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.PreviousClosingPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.UpperBoundPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.LowerBoundPrice)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.NumberOfTrades)
            .HasColumnType("int");
        builder.Property(x => x.TradingValue)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.TradingVolume)
            .HasColumnType("bigint");
        
        builder.HasOne(x => x.Instrument)
                .WithMany(x => x.TradingData)
                .HasForeignKey(x => x.InstrumentIsin)
                .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => x.InstrumentIsin);
    }
}
