
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteApi.DataAccess.Interfaces;
using PersonalWebsiteApi.DataAccess.Repositories;

namespace PersonalWebsiteApi.DataAccess;

public static class DIExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
