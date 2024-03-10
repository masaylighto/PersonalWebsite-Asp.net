
using Core.DataKit.Result;

namespace Core.HTTP.Interfaces;

public interface IResponseDeserializer<ReturnType>
{  /// <summary>
   ///  Specify how the http response will be Deserialize from the data received. 
   /// </summary>
    Task<ReturnType?> Deserialize(HttpResponseMessage httpResponseMessage);
}
