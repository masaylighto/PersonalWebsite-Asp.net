
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Cqrs;

public static class DIExtensions
{
    public static void AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
}
