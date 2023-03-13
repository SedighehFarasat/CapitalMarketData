﻿using CapitalMarketData.BackgroundTask.Helper;
using CapitalMarketData.Domain.Entities;
using CapitalMarketData.Domain.Enums;
using CapitalMarketData.Persistence;
using Microsoft.Extensions.Logging;

namespace CapitalMarketData.BackgroundTask.Services;
public class PersistService
{
    private readonly ILogger _logger;
    public readonly FileLogger _fileLogger;
    public readonly CapitalMarketDataDbContext _db;
    public readonly TseService _tseService;

    public PersistService(ILogger<PersistService> logger, CapitalMarketDataDbContext db, TseService tseService, FileLogger fileLogger)
    {
        _logger = logger;
        _fileLogger = fileLogger;
        _db = db;
        _tseService = tseService;
    }

    /// <summary>
    /// It stores today's trading data of an instrumnet in SQl Server.
    /// </summary>
    public void Persist()
    {
        var instruments = _db.Instruments.Select(x => new { x.Isin, x.Ticker }).ToArray();
        foreach (var instrument in instruments)
        {
            if (instrument.Isin is not null)
            {
                try
                {
                   var data = _tseService.FetchLiveData(instrument.Isin);
                    if (data != null)
                    {
                        TradingData tradingData = new()
                        {
                            InstrumentIsin = instrument.Isin,
                            Status = Convertor.ToStatusEnum(data.Result.header[0].state),
                        };
                        if (tradingData.Status == Status.Trading)
                        {
                            tradingData.OpeningPrice = decimal.Parse(data.Result.mainData.agh);
                            tradingData.HighestPrice = decimal.Parse(data.Result.mainData.bt.u);
                            tradingData.LowestPrice = decimal.Parse(data.Result.mainData.bt.d);
                            tradingData.LastPrice = decimal.Parse(data.Result.header[1].am);
                            tradingData.ClosingPrice = decimal.Parse(data.Result.mainData.ghp.v);
                            tradingData.PreviousClosingPrice = decimal.Parse(data.Result.mainData.rgh);
                            tradingData.UpperBoundPrice = decimal.Parse(data.Result.mainData.bm.u);
                            tradingData.LowerBoundPrice = decimal.Parse(data.Result.mainData.bm.d);
                            tradingData.NumberOfTrades = int.Parse(data.Result.mainData.dm.Replace(",", string.Empty));
                            tradingData.TradingValue = Convertor.ToNumber(data.Result.mainData.arm);
                            tradingData.TradingVolume = (long)Convertor.ToNumber(data.Result.mainData.hmo);
                        }
                        if (!_db.TradingData.Any(x => x.Date == tradingData.Date && x.InstrumentIsin == tradingData.InstrumentIsin))
                        {
                            _db.TradingData.Add(tradingData);
                            int affected = _db.SaveChanges();
                            _fileLogger.WriteToFile($"{affected} row affected for {instrument.Ticker}");
                            _logger.LogInformation("{affected} row affected for {Isin}", affected, instrument.Isin);
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    _fileLogger.WriteToFile($"Http Exception Caught On {instrument.Ticker}: {e.Message}");
                    _logger.LogError("Http Exception Caught On {Isin}: {errorMessage}", instrument.Isin, e.Message);
                }
                catch (Exception e)
                {
                    _fileLogger.WriteToFile($"Other Exception Caught On {instrument.Ticker}: {e.Message}");
                    _logger.LogError("Other Exception Caught On {Isin}: {errorMessage}", instrument.Isin, e.Message);
                }
                Thread.Sleep(3000);
            }
        }
    }
}




