

using Core.DataKit;
using Core.DataKit.Result;
using TheWayToGerman.Core.Database;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    public PostgresDBContext PostgresDBContext { get; }
    public IUserRepository UserRespository { get; set; }
    public UnitOfWork(PostgresDBContext postgresDBContext,IUserRepository userRespository)
    {
        PostgresDBContext = postgresDBContext;
        UserRespository = userRespository;
    }
  

    public async Task<Result<bool>> SaveAsync()
    {
        try
        {
          return await PostgresDBContext.SaveChangesAsync()>0;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
}
