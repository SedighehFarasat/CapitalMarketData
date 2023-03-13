using CapitalMarketData.Domain.Enums;

namespace CapitalMarketData.BackgroundTask.Helper;
public static class Convertor
{
    public static Status? ToStatusEnum(string str)
    {
        return str switch
        {
            "مجاز" => Status.Trading,
            "ممنوع-متوقف" => Status.Halted,
            _ => null,
        };
    }

    public static decimal? ToNumber(string str)
    {
        if (str.Contains('M'))
        {
            return decimal.Parse(str.Split(" ")[0]) * 1_000_000;
        }
        else if (str.Contains('B'))
        {
            return decimal.Parse(str.Split(" ")[0]) * 1_000_000_000;
        }
        else 
        { 
            return decimal.Parse(str);
        }
    }
}
