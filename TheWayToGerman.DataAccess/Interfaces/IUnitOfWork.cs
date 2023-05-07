
using Core.DataKit;
using Core.DataKit.Result;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IUserRespository UserRespository { get; set; }
    Task<Result<bool>> SaveAsync();
}
