
using Core.DataKit;
using Core.DataKit.Result;
using System.Linq.Expressions;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IUserRepository
{
    public Task<Result<User>> GetUserAsync(Func<User, bool> Where);
    public Task<bool> IsUserExistAsync(Func<User, bool> Where);
    public Task<Result<OK>> DeleteAdminById(Guid Id);
    public Task<Result<IEnumerable<T>>> GetUsersAsync<T>(Expression<Func<User, bool>> Where, Func<User, T> select);
    public Task<Result<Guid>> AddUserAsync(User user);
    public Task<Result<OK>> UpdateUserAsync(User user, Func<User, bool> Which);
}
