using Core.DataKit.Exceptions;
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
    async Task<Result<ReturnType>> SendAsync<ReturnType>(HttpRequestMessage httpRequestMessage, IResponseDeserializer<ReturnType> serializationStrategy) where ReturnType : class
    {
        try
        {
            //send request -> receive response  -> try to serialize data. return either the data or exception that hold the detail of the error
            var response = await HttpClient.SendAsync(httpRequestMessage);
            var responseDeserialized = await serializationStrategy.Deserialize(response);
            return responseDeserialized is not null ? responseDeserialized : new NoResponseException("faild to deserialize response ");
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
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.Method = method;
        httpRequestMessage.RequestUri = new Uri(endPoint);
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
    public async Task<Result<ReturnType>> PostAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(endPoint, HttpMethod.Post, content, HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, responseDeserializer);
    }

    public async Task<Result<ReturnType>> PutAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(endPoint, HttpMethod.Put, content, HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, responseDeserializer);
    }

    public async Task<Result<ReturnType>> DeleteAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(endPoint, HttpMethod.Delete, content, HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, responseDeserializer);
    }

    public async Task<Result<ReturnType>> GetAsync<ReturnType, RequestContent>(string endPoint, IResponseDeserializer<ReturnType> responseDeserializer, RequestContent? content = default, IRequestContentBuilder<RequestContent>? HTTPContentBuilder = null) where ReturnType : class where RequestContent : class
    {
        //build request if created send if not return the error
        var requestContent = await BuildMessageContent(endPoint, HttpMethod.Get, content, HTTPContentBuilder);
        if (requestContent.ContainError())
        {
            return requestContent.GetError();
        }
        HttpRequestMessage httpRequestMessage = requestContent.GetData();
        return await SendAsync(httpRequestMessage, responseDeserializer);
    }

}
