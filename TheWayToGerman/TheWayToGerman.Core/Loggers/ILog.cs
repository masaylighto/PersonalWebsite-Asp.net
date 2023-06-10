
namespace TheWayToGerman.Core.Loggers;

public interface ILog
{
    public void Warn(string message,params object[] parameters);
    public void Error(string message, params object[] parameters);
    public void Error(Exception message);
    public void Info(string message, params object[] parameters);
    public void Debug(string message, params object[] parameters);

}
