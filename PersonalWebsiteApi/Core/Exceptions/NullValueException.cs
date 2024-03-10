
namespace PersonalWebsiteApi.Core.Exceptions;

public class NullValueException : Exception
{
    public NullValueException() : base()
    {
    }

    public NullValueException(string? message) : base(message)
    {
    }

    public NullValueException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
