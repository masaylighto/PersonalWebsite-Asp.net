
namespace TheWayToGerman.Core.Loggers;

public interface ILog
{
    public void Warn(string message);
    public void Error(string message);
    public void Error(Exception message);
    public void Info(string message);
    public void Debug(string message);

}
