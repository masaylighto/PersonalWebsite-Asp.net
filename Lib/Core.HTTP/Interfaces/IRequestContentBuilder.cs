using Core.DataKit.Result;

namespace Core.HTTP.Interfaces;

public interface IRequestContentBuilder<ContentType> where ContentType : class
{
    /// <summary>
    ///  Specify how the http content will be builded from the data passed into the class
    /// </summary>
    Task<Result<HttpContent>> BuildContent(ContentType contentType);
}
