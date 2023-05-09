using Core.Cqrs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using TheWayToGerman.Core.Exceptions;
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
        var authConfig = configuration.GetSection("JWT").Get<AuthConfig>();
        if (authConfig is null)
        {
            throw new NullValueException("AuthConfig is null");           
        }
        services.AddSingleton(authConfig);
        services.AddScoped<IAuthService, JwtService>();


        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = authConfig.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SigningKey))
            };
        });


    }
}
