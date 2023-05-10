

using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace IntegrationTest;

public static class Helper
{
    public static StringContent CreateJsonContent<Type>(Type Data)
    {

        return new StringContent(JsonConvert.SerializeObject(Data),new MediaTypeHeaderValue("application/json"));
    }
}
