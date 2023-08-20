using Core.DataKit;
using Core.DataKit.Exceptions;
using Core.DataKit.Result;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Loggers;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    public PostgresDBContext PostgresDBContext { get; }
    public IUserRepository UserRespository { get; set; }
    public ICategoryRepository CategoriesRepository { get; set; }
    public IArticleRepository ArticleRepository { get; set; }
    public ILog Log { get; }
    public UnitOfWork(PostgresDBContext postgresDBContext, IUserRepository userRespository, ICategoryRepository categoriesRepository, IArticleRepository articleRepository, ILog log)
    {
        PostgresDBContext = postgresDBContext;
        UserRespository = userRespository;
        CategoriesRepository = categoriesRepository;
        ArticleRepository = articleRepository;
        Log = log;
    }


    public async Task<Result<OK>> SaveAsync()
    {
        try
        {
            if (await PostgresDBContext.SaveChangesAsync() == 0)
            {
                return new DBNoChangesException("No Changes Applied");
            }
            return new OK();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : failed save changes");
        }
    }
}
