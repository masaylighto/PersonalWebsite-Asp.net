
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TheWayToGerman.Api.DTO.Login;
using TheWayToGerman.Api.ResponseObject.Login;
using TheWayToGerman.Core.Database;

namespace IntegrationTest;

public static class WebApplicationBuilder
{
    public static HttpClient ApiClient()
    {
        return new WebApplicationFactory<Program>().WithWebHostBuilder(HostConfiguration).CreateClient();
       
    }
    static void HostConfiguration(IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.ConfigureServices(services =>
        {
            var DBservice = services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<PostgresDBContext>));
            if (DBservice is not null)
            {
                services.Remove(DBservice);
            }
            string memoryInstanceName = Guid.NewGuid().ToString();// guid so it will be unique to every instance of the class   
            var option = new DbContextOptionsBuilder<PostgresDBContext>().UseInMemoryDatabase(memoryInstanceName).Options;
            new PostgresDBContext(option).Database.EnsureCreated();
            services.AddDbContextPool<PostgresDBContext>((x) => 
            {
                x.UseInMemoryDatabase(memoryInstanceName);          
                
            });
          
        });

    }
    public static async Task Authenticate(this HttpClient httpClient ,string username = "masaylighto", string password = "8PS33gotf24a")
    {

        var dto = new AuthenticateDTO() { Username = username, Password = password };
        var req = JsonConvert.SerializeObject(dto);


        using var result = await httpClient.PostAsync("v1/login/auth",new StringContent(req, new MediaTypeHeaderValue("application/json")));
        
        var response = await result.Content.ReadAsStringAsync();
        
        try
        {          
            var json = JsonConvert.DeserializeObject<AuthenticateResponse>(response);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {json.JwtToken}");
        }
        catch (Exception ex)
        {
            throw new Exception($" the http response {response} : the error {ex.Message} ");
        }
  
    }
    public static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, string url,HttpContent content, HttpMethod method)
    {
        var req = new HttpRequestMessage()
        {
            RequestUri = new Uri(httpClient.BaseAddress + url),
            Content = content,
            Method = method
        };
        req.Headers.Authorization = httpClient.DefaultRequestHeaders.Authorization; // this fake client act very wired 
        return await httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
    } 
}
