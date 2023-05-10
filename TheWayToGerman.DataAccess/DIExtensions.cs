
using Microsoft.Extensions.DependencyInjection;
using TheWayToGerman.DataAccess.Interfaces;
using TheWayToGerman.DataAccess.Repositories;

namespace TheWayToGerman.DataAccess;

public static class DIExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
