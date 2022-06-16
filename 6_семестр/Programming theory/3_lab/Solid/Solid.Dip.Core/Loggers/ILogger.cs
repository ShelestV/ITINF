namespace Solid.Dip.Loggers;

public interface ILogger
{
    void Info(string msg);
    void Error(string msg, Exception ex);
}
