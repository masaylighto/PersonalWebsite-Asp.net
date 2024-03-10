

using Core.HTTP.Interfaces;
using Core.HTTP.ResponseDeserializers;

namespace Core.HTTP;

public class Request<RequestContent,SuccessReturn,ErrorReturn> where SuccessReturn : class where ErrorReturn : class where RequestContent : class
{
    public virtual required string Endpoint { get; set; }
    public virtual IResponseDeserializer<SuccessReturn, ErrorReturn> ResponseDeserializer { get; set; } = new JsonResponseDeserializer<SuccessReturn, ErrorReturn>();
    public virtual RequestContent? Content { get; set; }
    public virtual IRequestContentBuilder<RequestContent>? HTTPContentBuilder { get; set; }
}
