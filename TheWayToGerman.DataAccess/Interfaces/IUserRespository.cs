
using Core.DataKit;
using Core.DataKit.Result;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IUserRepository
{
    public Task<Result<User>> GetUserAsync(Func<User, bool> Where);
    public Task<Result<Guid>> AddUserAsync(User user);
    public Task<Result<OK>> UpdateUserAsync(User user, Func<User, bool> Which);
}
