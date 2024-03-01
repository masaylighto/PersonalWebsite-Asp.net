
namespace PersonalWebsiteApi.Core.Exceptions;

public class FileAlreadyExistException : IOException
{
    public FileAlreadyExistException() : base()
    {
    }

    public FileAlreadyExistException(string? message) : base(message)
    {
    }

    public FileAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
