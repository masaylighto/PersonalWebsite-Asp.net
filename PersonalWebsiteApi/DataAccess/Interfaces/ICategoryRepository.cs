
using Core.DataKit;
using Core.DataKit.Result;
using System.Linq.Expressions;
using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.DataAccess.Interfaces;

public interface ICategoryRepository
{
    public Task<Result<bool>> IsExistAsync(Func<Category, bool> predictate);
    public Task<Result<OK>> AddAsync(Category category);
    public Result<IAsyncEnumerable<T>> GetAsync<T>(Expression<Func<Category, bool>> predictate, Func<Category, T> selector);
    public Task<Result<Category>> GetAsync(Expression<Func<Category, bool>> predictate);
}
