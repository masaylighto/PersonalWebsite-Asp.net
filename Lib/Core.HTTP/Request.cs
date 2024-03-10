

using Core.HTTP.Interfaces;
using Core.HTTP.ResponseDeserializers;

namespace Core.HTTP;

public class Request<RequestContent,ReturnType> where ReturnType : class where RequestContent : class
{
    public virtual required string Endpoint { get; set; }
    public virtual IResponseDeserializer<ReturnType> ResponseDeserializer { get; set; } = new JsonResponseDeserializer<ReturnType>();
    public virtual RequestContent? Content { get; set; }
    public virtual IRequestContentBuilder<RequestContent>? HTTPContentBuilder { get; set; }
}
