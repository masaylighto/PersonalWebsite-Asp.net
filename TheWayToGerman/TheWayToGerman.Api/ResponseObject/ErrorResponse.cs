namespace TheWayToGerman.Api.ResponseObject;

public class ErrorResponse
{
    public required string Error { get; set; }
    public static ErrorResponse From(string message) => new() { Error = message };
}
