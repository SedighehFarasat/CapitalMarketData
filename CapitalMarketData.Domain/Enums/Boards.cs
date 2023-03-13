namespace CapitalMarketData.Domain.Enums;
public enum Board
{
    /// <summary>
    /// بورس - بازار اول - تابلو اصلی 
    /// </summary>
    Exchange_FirstMarket_MajorBoard = 11,

    /// <summary>
    /// بورس - بازار اول - تابلو فرعی 
    /// </summary>
    Exchange_FirstMarket_MinorBoard = 13,

    /// <summary>
    /// بورس - بازار دوم 
    /// </summary>
    Exchange_SecondMarket_ = 15,

    /// <summary>
    /// فرابورس - بازار اول 
    /// </summary>
    OffExchange_FirstMarket_ = 21,

    /// <summary>
    /// فرابورس - بازار دوم 
    /// </summary>
    OffExchange_SecondMarket_ = 23,

    /// <summary>
    /// فرابورس - بازار پایه 
    /// </summary>
    OffExchange_BaseMarket = 27,

    /// <summary>
    /// فرابورس - بازار شرکت های کوچک و متوسط 
    /// </summary>
    OffExchange_SMEMarket = 29,

    /// <summary>
    /// فرابورس - بازار ابزارهای نوین 
    /// </summary>
    OffExchange_ModernInstrumentsMarket = 24,
}
