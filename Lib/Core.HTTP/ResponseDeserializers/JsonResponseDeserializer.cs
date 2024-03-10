using Core.DataKit;
using Core.DataKit.Result;
using Core.HTTP.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Core.HTTP.ResponseDeserializers;

public class JsonResponseDeserializer<SuccessReturn, ErrorReturn> : IResponseDeserializer<SuccessReturn, ErrorReturn> where SuccessReturn : class where ErrorReturn : class
{
    public async Task<ResponseWrapper<SuccessReturn, ErrorReturn>> Deserialize(HttpResponseMessage httpResponseMessage)
    {
       
        using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();       
        var response = new ResponseWrapper<SuccessReturn, ErrorReturn>()
        {
            IsSuccess = httpResponseMessage.IsSuccessStatusCode,
            StatusCode = httpResponseMessage.StatusCode
        };
        if (stream.Length==0)
        {
            return response;
        }
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                response.Error = await JsonSerializer.DeserializeAsync<ErrorReturn>(stream, options);
                return response;
            }
            response.Success = await JsonSerializer.DeserializeAsync<SuccessReturn>(stream, options);
            return response;
        }
        catch (Exception ex)
        {   
            var bytes= new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);     
            var responseContent = Encoding.UTF8.GetString(bytes);
            throw new Exception($"ResponseContent {responseContent}",ex);
        }
    }
}
