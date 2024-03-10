using Core.DataKit.Result;
using Core.HTTP.Interfaces;

namespace Core.HTTP;

public class GenericHttpClient : IGenericHttpClient
{
    HttpClient HttpClient { get; }
    public GenericHttpClient()
    {
        HttpClient = new HttpClient();

    }
    public GenericHttpClient(HttpClient httpClient)
    {
        HttpClient = httpClient;

    }
    public void SetAuthStrategy(IAuthStrategy authStrategy)
    {
        authStrategy.ApplyAuth(HttpClient.DefaultRequestHeaders);
    }

    public void SetHeader(string Key, string Value)
    {
        if (HttpClient.DefaultRequestHeaders.Contains(Key))
        {
            HttpClient.DefaultRequestHeaders.Remove(Key);
        }
        HttpClient.DefaultRequestHeaders.Add(Key, Value);
    }
    public void SetBaseUrl(string baseUrl)
    {
        HttpClient.BaseAddress = new Uri(baseUrl);
    }
    async Task<Result<ResponseWrapper<ReturnType>>> SendAsync<ReturnType>(HttpRequestMessage httpRequestMessage, IResponseDeserializer<ReturnType> deserializationStrategy) where ReturnType : class
    {
        try
        {
            //send request -> receive response  -> try to serialize data. return either the data or exception that hold the detail of the error
            var response = await HttpClient.SendAsync(httpRequestMessage);        
            var responseDeserialized = await deserializationStrategy.Deserialize(response);
            return new ResponseWrapper<ReturnType>() 
            {
                Response = responseDeserialized,
                StatusCode = response.StatusCode,
            };
        }
        catch (Exception ex)
        {
            return ex;
        }

    }
    async Task<Result<HttpRequestMessage>> BuildMessageContent<RequestContent>(string endPoint, HttpMethod method, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where RequestContent : class
    {
        // create a http request message -> specify its information -> check if there is content ->
        // convert them into request content if succeed return HttpRequestMessage if failed return exception holding the detail of the error
        HttpRequestMessage httpRequestMessage = new()
        {
            Method = method,
            RequestUri = new Uri(HttpClient.BaseAddress + endPoint)
        };
        if (content is not null && HTTPContentBuilder is not null)
        {
            var requestContent = await HTTPContentBuilder.BuildContent(content!);
            if (requestContent.ContainError())
            {
                return requestContent.GetError();
            }
            httpRequestMessage.Content = requestContent.GetData();
        }
        return httpRequestMessage;
    }
    public async Task<Result<ResponseWrapper<ReturnType>>> PostAsync<RequestContent,ReturnType>(Request<RequestContent,ReturnType> request) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(request.Endpoint, HttpMethod.Post, request.Content, request.HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, request.ResponseDeserializer);
    }

    public async Task<Result<ResponseWrapper<ReturnType>>> PutAsync<RequestContent,ReturnType>(Request<RequestContent,ReturnType> request) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(request.Endpoint, HttpMethod.Put, request.Content, request.HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, request.ResponseDeserializer);
    }

    public async Task<Result<ResponseWrapper<ReturnType>>> DeleteAsync<RequestContent,ReturnType>(Request<RequestContent,ReturnType> request) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(request.Endpoint, HttpMethod.Delete, request.Content, request.HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, request.ResponseDeserializer);
    }

    public async Task<Result<ResponseWrapper<ReturnType>>> GetAsync<RequestContent,ReturnType>(Request<RequestContent,ReturnType> request) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(request.Endpoint, HttpMethod.Get, request.Content, request.HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, request.ResponseDeserializer);
    }

}
