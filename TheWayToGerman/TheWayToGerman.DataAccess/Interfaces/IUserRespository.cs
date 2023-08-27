
using Core.DataKit;
using Core.DataKit.Result;
using System.Linq.Expressions;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IUserRepository
{
    public Task<Result<User>> GetAsync(Func<User, bool> predictate);
    public Task<Result<OK>> IsUserExistAsync(Expression<Func<User, bool>> predictate);
    public Task<Result<OK>> DeleteAdminById(Guid Id);
    public Result<IAsyncEnumerable<T>> GetUsers<T>(Expression<Func<User, bool>> predictate, Func<User, T> selector, int pageSize, int pageNumber);
    public Task<Result<Guid>> AddUserAsync(User user);
    public Task<Result<OK>> UpdateUserAsync(User user, Func<User, bool> predictate);
}
