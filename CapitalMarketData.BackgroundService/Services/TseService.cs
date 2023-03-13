using CapitalMarketData.BackgroundTasks.Models;
using System.Text.Json;

namespace CapitalMarketData.BackgroundTasks.Services;
public class TseService
{
    /// <summary>
    /// It gets today trading data for specified ISIN from tse.ir API.
    /// </summary>
    /// <param name="isin">12 digits code of an instrument e.g. IRO1FKHZ0001.</param>
    /// <returns>Trading data as Trade type.</returns>
    public async Task<Trade?> FetchLiveData(string isin)
    {
        var url = $@"https://tse.ir/json/Instrument/info_{isin}.json";
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        // TODO: Retry sending request in case of failing.
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        Trade? instrument = JsonSerializer.Deserialize<Trade>(responseString);
        return instrument;
    }
}
