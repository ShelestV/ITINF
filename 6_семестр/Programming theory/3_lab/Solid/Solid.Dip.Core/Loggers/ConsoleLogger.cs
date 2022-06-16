namespace Solid.Dip.Loggers;

public sealed class ConsoleLogger : ILogger
{
    private static readonly ConsoleLogger instance = new();
    public static ConsoleLogger Instance => instance;

    private ConsoleLogger()
    {
    }

    public void Error(string msg, Exception ex)
    {
        Console.WriteLine($"Error: {msg}; {ex.Message}\n{ex.StackTrace}");
    }

    public void Info(string msg)
    {
        Console.WriteLine($"Info: {msg}");
    }
}
