
using Core.DataKit;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;
using System.Linq.Expressions;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IArticleRepository
{
    public Task<Result<bool>> IsExistAsync(Func<Article, bool> predictate);
    public Task<State> AddAsync(Article article);
    public Task<State> UpdateAsync(Article article);
    public Result<IAsyncEnumerable<T>> GetAsync<T>(Expression<Func<Article, bool>> predictate, Func<Article, T> selector);
    public Task<Result<Article>> GetAsync(Guid Id);
}
