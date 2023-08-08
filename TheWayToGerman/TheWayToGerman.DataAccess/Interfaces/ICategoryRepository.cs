
using Core.DataKit;
using Core.DataKit.Result;
using System.Linq.Expressions;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface ICategoryRepository
{
    public Task<Result<bool>> IsExistAsync(Func<Category, bool> predictate);
    public Task<Result<OK>> AddAsync(Category category);
    public Result<IEnumerable<T>> Get<T>(Expression<Func<Category, bool>> predictate, Func<Category, T> selector);
}
