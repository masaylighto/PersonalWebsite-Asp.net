
using Core.DataKit.Result;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IUserRespository
{
    public Task<Result<User>> GetUserAsync(Func<User, bool> Where);
    public Task<Result<Guid>> AddUserAsync(User user);
}
