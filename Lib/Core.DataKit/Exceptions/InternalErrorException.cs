
using System.Runtime.CompilerServices;

namespace Core.DataKit.Exceptions;

public class InternalErrorException:Exception
{
    public InternalErrorException() : base()
    {
    }

    public InternalErrorException(string? message) : base(message)
    {
    }

    public InternalErrorException( Exception? innerException, string message = "Internel Error") : base(message, innerException)
    {
    }
}
