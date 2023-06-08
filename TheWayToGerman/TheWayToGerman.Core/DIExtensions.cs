using Core.DataKit.MockWrapper;
using Humanizer.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Loggers;
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
    public static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        ILogger logger= new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
        services.AddSingleton(logger);
        services.AddTransient<ILog, SerilogLogger>();
    }

}
