using System.Globalization;

namespace CapitalMarketData.BackgroundTask.Services;
public class FileLogger
{
    private readonly string _filePath;

    public FileLogger()
    {
        string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        Directory.CreateDirectory(dirPath);
        PersianCalendar pc = new();
        _filePath = Path.Combine(dirPath, $"{pc.GetYear((DateTime.Now))}-{pc.GetMonth((DateTime.Now))}-{pc.GetDayOfMonth((DateTime.Now))}.txt");
    }
    public void WriteToFile(string message)
    {
        using(StreamWriter textWriter = File.AppendText(_filePath))
        {
            textWriter.WriteLine(message);
        }
    }
}
