using Core.DataKit;
using Core.DataKit.Result;
using Core.HTTP.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Core.HTTP.ResponseDeserializers;

public class JsonResponseDeserializer<ReturnType> : IResponseDeserializer<ReturnType>
{
    public async Task<ReturnType?> Deserialize(HttpResponseMessage httpResponseMessage)
    {
        
            using MemoryStream stream = new MemoryStream();
            await httpResponseMessage.Content.CopyToAsync(stream);
            if (stream.Length==0)
            {
                return default;              
            }
            var result = await JsonSerializer.DeserializeAsync<ReturnType>(stream);
            return result;


    }
}
