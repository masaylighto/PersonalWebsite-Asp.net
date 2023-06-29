
using Serilog;

namespace TheWayToGerman.Core.Loggers;

public class SerilogLogger : ILog
{
    public ILogger Logger { get; }
    public SerilogLogger(Serilog.ILogger logger)
    {
        Logger = logger;
    } 

    public void Debug(string message, params object[] parameters)
    {
        Logger.Debug(message, parameters);
    }

    public void Error(string message, params object[] parameters)
    {
        Logger.Error(message, parameters);
    }

    public void Error(Exception message)
    {
        Logger.Error("Source: {source} ; Message {Message} ; Inner Message {Message}", message.Source, message.Message, message.InnerException?.Message);
    }

    public void Info(string message, params object[] parameters)
    {
        Logger.Information(message, parameters);
    }

    public void Warn(string message, params object[] parameters)
    {
        Logger.Warning(message, parameters);
    }
}
