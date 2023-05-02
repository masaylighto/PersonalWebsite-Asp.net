using Core.DataKit.Result;
using Core.HTTP.Interfaces;
using System.Text.Json;

namespace Core.HTTP.ResponseDeserializers;

public class JsonResponseDeserializer<ReturnType> : IResponseDeserializer<ReturnType>
{
    public async Task<Result<ReturnType>> Deserialize(HttpResponseMessage httpResponseMessage)
    {
        try
        {
            var result = await JsonSerializer.DeserializeAsync<ReturnType>(httpResponseMessage.Content.ReadAsStream());
            return result is not null ? result : new NullReferenceException("Empty json response after Deserializing");
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
