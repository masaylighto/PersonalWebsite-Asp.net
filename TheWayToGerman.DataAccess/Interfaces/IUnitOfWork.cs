
using Core.DataKit;
using Core.DataKit.Result;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRespository { get; set; }
    Task<Result<bool>> SaveAsync();
}
