

namespace TheWayToGerman.Core.Exceptions;
/// <summary>
///  Used to indecate that there is no changes on data happen to database
/// </summary>
public class DBNoChangesException:Exception
{
    public DBNoChangesException() : base()
    {
    }

    public DBNoChangesException(string? message) : base(message)
    {
    }

    public DBNoChangesException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
