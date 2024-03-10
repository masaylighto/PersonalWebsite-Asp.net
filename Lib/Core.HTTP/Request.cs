

using Core.HTTP.Interfaces;
using Core.HTTP.ResponseDeserializers;

namespace Core.HTTP;

public class Request<ReturnType, RequestContent> where ReturnType : class where RequestContent : class
{
    public required string Endpoint { get; set; }
    public IResponseDeserializer<ReturnType> ResponseDeserializer { get; set; } = new JsonResponseDeserializer<ReturnType>();
    public RequestContent? Content { get; set; }
    public IRequestContentBuilder<RequestContent>? HTTPContentBuilder { get; set; }
}
