
using Serilog;

namespace TheWayToGerman.Core.Loggers;

public class SerilogLogger : ILog
{
    public ILogger Logger { get; }
    public SerilogLogger(Serilog.ILogger logger)
    {
        Logger = logger;
    } 

    public void Debug(string message)
    {
        Logger.Debug(message);
    }

    public void Error(string message)
    {
        Logger.Error(message);
    }

    public void Error(Exception message)
    {
        Logger.Error($"Source: {message.Source} ; Message {message.Message} ; Inner Message {message.InnerException?.Message}");
    }

    public void Info(string message)
    {
        Logger.Information(message);
    }

    public void Warn(string message)
    {
        Logger.Warning(message);
    }
}
