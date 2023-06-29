using Core.DataKit;
using Core.DataKit.Exceptions;
using Core.DataKit.Result;
using Serilog;
using Serilog.Core;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    public PostgresDBContext PostgresDBContext { get; }
    public IUserRepository UserRespository { get; set; }
    public ICategoryRepository CatagoriesRepository{ get; set; }
    public ILanguageRepository LanguageRepository { get; set; }
    public UnitOfWork(PostgresDBContext postgresDBContext, IUserRepository userRespository, ILanguageRepository languageRepository, ICategoryRepository catagoriesRepository)
    {
        PostgresDBContext = postgresDBContext;
        UserRespository = userRespository;
        LanguageRepository = languageRepository;
        CatagoriesRepository = catagoriesRepository;
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
            return  new InternalErrorException(ex, "Error on our side : failed save changes");
        }
    }
}
