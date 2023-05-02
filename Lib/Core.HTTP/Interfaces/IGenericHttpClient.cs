
using Core.DataKit;
using Core.DataKit.Result;

namespace Core.HTTP.Interfaces;
/// <summary>
/// Generic Http Client that make use of strategy pattern to handle different scenario.
/// </summary>
public interface IGenericHttpClient
{
    
    public void SetBaseUrl(string baseUrl);    
    /// <summary>
    /// Apply Header to http request
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Value"></param>
    public void SetHeader(string Key,string Value);
    /// <summary>
    /// Apply authentication Startgy. like Jwt Token etc 
    /// </summary>
    /// <param name="authStrategy"></param>
    public void SetAuthStrategy(IAuthStrategy authStrategy);
    /// <summary>
    ///  Make POST HTTP Request 
    /// </summary>
    /// <typeparam name="ReturnType">Type Of Return Data</typeparam>
    /// <typeparam name="RequestContent">Type Of Passed Data</typeparam>
    /// <param name="endPoint"> Api end point</param>
    /// <param name="responseDeserializer">specify how http response content will be deserialized</param>
    /// <param name="content">the request content</param>
    /// <param name="HTTPContentBuilder">specify how request content will be serialized</param>
    /// <returns></returns>
    public Task<Result<ReturnType>> PostAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder =null) where ReturnType : class where RequestContent :class;
    /// <summary>
    ///  Make PUT HTTP Request 
    /// </summary>
    /// <typeparam name="ReturnType">Type Of Return Data</typeparam>
    /// <typeparam name="RequestContent">Type Of Passed Data</typeparam>
    /// <param name="endPoint"> Api end point</param>
    /// <param name="responseDeserializer">specify how http response content will be deserialized</param>
    /// <param name="content">the request content</param>
    /// <param name="HTTPContentBuilder">specify how request content will be serialized</param>
    /// <returns></returns>
    public Task<Result<ReturnType>> PutAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where ReturnType : class where RequestContent : class;
    /// <summary>
    ///  Make Delete HTTP Request 
    /// </summary>
    /// <typeparam name="ReturnType">Type Of Return Data</typeparam>
    /// <typeparam name="RequestContent">Type Of Passed Data</typeparam>
    /// <param name="endPoint"> Api end point</param>
    /// <param name="responseDeserializer">specify how http response content will be deserialized</param>
    /// <param name="content">the request content</param>
    /// <param name="HTTPContentBuilder">specify how request content will be serialized</param>
    /// <returns></returns>
    public Task<Result<ReturnType>> DeleteAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where ReturnType : class where RequestContent :class;
    /// <summary>
    ///  Make GET HTTP Request 
    /// </summary>
    /// <typeparam name="ReturnType">Type Of Return Data</typeparam>
    /// <typeparam name="RequestContent">Type Of Passed Data</typeparam>
    /// <param name="endPoint"> Api end point</param>
    /// <param name="responseDeserializer">specify how http response content will be deserialized</param>
    /// <param name="content">the request content</param>
    /// <param name="HTTPContentBuilder">specify how request content will be serialized</param>
    /// <returns></returns>
    public Task<Result<ReturnType>> GetAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where ReturnType : class where RequestContent : class;
}
