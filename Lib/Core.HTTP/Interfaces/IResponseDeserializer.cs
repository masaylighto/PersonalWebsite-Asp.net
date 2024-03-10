
using Core.DataKit.Result;

namespace Core.HTTP.Interfaces;

public interface IResponseDeserializer<SuccessReturn,ErrorReturn> where SuccessReturn : class where ErrorReturn : class
{  /// <summary>
   ///  Specify how the http response will be Deserialize from the data received. 
   /// </summary>
    Task<ResponseWrapper<SuccessReturn, ErrorReturn>> Deserialize(HttpResponseMessage httpResponseMessage);
}
