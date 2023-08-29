namespace TextSorter;

public class TextSorterConsoleLogger : ITextSorterLogger
{
    public void Log(string message)
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = loggerFactory.CreateLogger<TextSorter>();
        logger.LogError($"{message} at {DateTime.Now}");
    }
}