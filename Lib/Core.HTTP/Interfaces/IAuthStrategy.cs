using System.Net.Http.Headers;
namespace Core.HTTP.Interfaces;

public interface IAuthStrategy
{
    /// <summary>
    ///  Implement how you want the Auth to apply to header in this method
    /// </summary>
    /// <param name="headers"></param>
    void ApplyAuth(HttpRequestHeaders headers);
}