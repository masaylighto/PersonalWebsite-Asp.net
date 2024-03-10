using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PersonalWebsiteApi.Core.Cqrs.Commands;
using PersonalWebsiteApi.Core.Cqrs.Queries;
using PersonalWebsiteApi.Core.Database;
using PersonalWebsiteApi.Logic.Authentication;
using Core.HTTP;
using Core.HTTP.Interfaces;

namespace IntegrationTest;

public static class WebApplicationBuilder
{
    public static IGenericHttpClient ApiClient()
    {
        var httpclient= new WebApplicationFactory<Program>().WithWebHostBuilder(HostConfiguration).CreateClient();
        return new GenericHttpClient(httpclient);
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
}
