
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContextPool<PostgresDBContext>((x) => {
                x.UseInMemoryDatabase("IntegrationTestDB");
               
                
                });

        });

    }
}
