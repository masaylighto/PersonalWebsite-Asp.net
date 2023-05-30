using Core.DataKit.MockWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.ParametersBinders;

namespace TheWayToGerman.Core;

public static class DIExtensions
{
    public static void AddPostgresDB(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddDbContextPool<PostgresDBContext>(option => option.UseNpgsql(configuration.GetConnectionString("PostgreSql")), configuration.GetValue<int>("PostgreSqlPool"));
        var options = new DbContextOptionsBuilder<PostgresDBContext>().UseNpgsql(configuration.GetConnectionString("PostgreSql")).Options;
        var context = new PostgresDBContext(options);
        if (context.Database.CanConnect())
        {
            context.Database.Migrate();
        } 
       
    }
    public static void AddDataTimeProvider(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
  
}
