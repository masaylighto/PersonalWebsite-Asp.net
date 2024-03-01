
namespace PersonalWebsiteApi.Core.Exceptions;

public class UniqueFieldException : Exception
{
    public UniqueFieldException() : base()
    {
    }

    public UniqueFieldException(string? message) : base(message)
    {
    }

    public UniqueFieldException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
