using Core.Cqrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using TheWayToGerman.Logic.Authentication;

namespace TheWayToGerman.Logic;

public static class DIExtensions
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
    public static void AddJWTAuth(this IServiceCollection services,IConfiguration configuration)
    {
        configuration = configuration.GetSection("JWT");
        var authConfig = new AuthConfig 
        {
            MinutesToExpire = int.Parse(configuration["MinuteToExpire"]),
            TokenKey = configuration["Token"]
        };
        services.AddSingleton(authConfig);
        services.AddScoped<IAuthService, JwtService>();
    }
}
