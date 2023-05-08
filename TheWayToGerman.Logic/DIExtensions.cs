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
            MinutesToExpire = int.Parse(configuration["MinuteToExpire"]!), // if it is  null it will through and inform the developer (the dev not the user as it will happen on the first server run)
            TokenKey = configuration["Token"] ?? throw new NullReferenceException("JWTToken is null")
        };
        services.AddSingleton(authConfig);
        services.AddScoped<IAuthService, JwtService>();
    }
}
