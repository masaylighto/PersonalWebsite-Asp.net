using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheWayToGerman.Core.Database;

namespace TheWayToGerman.Core;

public static class DIExtensions
{
    public static void AddPostgresDB(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<PostgresDBContext>(option => option.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
    }
}
