
namespace Core.HTTP
{
    internal class NoResponseException : Exception
    {
        public NoResponseException() : base()
        {
        }

        public NoResponseException(string? message) : base(message)
        {
        }

        public NoResponseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}