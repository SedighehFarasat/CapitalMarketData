namespace CapitalMarketData.BackgroundTasks.Models;
public class Trade  
{
    public string ci { get; set; }
    public object inP { get; set; }
    public object cat { get; set; }
    public object a_tag { get; set; }
    public List<Header> header { get; set; }
    public MainData mainData { get; set; }
    public object futureData { get; set; }
    public object optionData { get; set; }
    public object ITables { get; set; }
    public object co { get; set; }
}