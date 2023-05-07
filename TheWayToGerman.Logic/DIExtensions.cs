using Core.Cqrs;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace TheWayToGerman.Logic;

public static class DIExtensions
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
