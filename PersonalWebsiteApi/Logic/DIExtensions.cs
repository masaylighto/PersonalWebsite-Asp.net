using Core.Cqrs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using PersonalWebsiteApi.Core.Configuration;
using PersonalWebsiteApi.Core.Enums;
using PersonalWebsiteApi.Core.Exceptions;
using PersonalWebsiteApi.Core.Helpers;
using PersonalWebsiteApi.Logic.Authentication;

namespace PersonalWebsiteApi.Logic;

public static class DIExtensions
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
    public static void AddJWTAuth(this IServiceCollection services, IConfiguration configuration)
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
        services.AddAuthorizationBuilder().AddPolicy(AuthPolicies.OwnerPolicy, p => p.RequireRole(nameof(UserType.Owner)));
        services.AddAuthorizationBuilder().AddPolicy(AuthPolicies.AdminPolicy, p => p.RequireRole(nameof(UserType.Admin)));
        services.AddAuthorizationBuilder().AddPolicy(AuthPolicies.AdminsAndOwners, p => p.RequireRole(nameof(UserType.Owner), nameof(UserType.Admin)));
    }

    public static void AddRateLimiters(this IServiceCollection services, IConfiguration configuration)
    {
        var configurations = configuration.GetSection("Limiters").Get<IEnumerable<RateLimiterConfig>>();
        if (configurations is null)
        {
            throw new NullValueException("Limiters are null");
        }
        foreach (var config in configurations)
        {
            services.AddRateLimiter(rate => rate
            .AddFixedWindowLimiter(policyName: config.PolicyName, options =>
            {
                options.QueueLimit = config.QueueLimit;
                options.PermitLimit = config.PermitLimit;
                options.QueueProcessingOrder = Enum.Parse<QueueProcessingOrder>(config.QueueProcessingOrder);
                options.Window = TimeSpan.FromMinutes(config.TimeWindowInMinute);
            }));
        }

    }


}
