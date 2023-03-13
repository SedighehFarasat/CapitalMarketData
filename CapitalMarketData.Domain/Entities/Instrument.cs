using CapitalMarketData.Domain.Enums;

namespace CapitalMarketData.Domain.Entities;
public class Instrument
{
    public Instrument()
    {
        TradingData = new HashSet<TradingData>();
    }

    public string Isin { get; set; }
    public string InsCode { get; set; }
    public string Ticker { get; set; }
    public string Name { get; set; }
    public Board? Board { get; set; }
    public Industry? Industry { get; set; }

    public ICollection<TradingData> TradingData { get; set; }
}