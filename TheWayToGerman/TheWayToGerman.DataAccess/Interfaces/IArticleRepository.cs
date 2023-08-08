
using Core.DataKit;
using Core.DataKit.Result;
using System.Linq.Expressions;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.DataAccess.Interfaces;

public interface IArticleRepository
{
    public Task<Result<bool>> IsExistAsync(Func<Article, bool> predictate);
    public Task<Result<OK>> AddAsync(Article category);
    public Result<IEnumerable<T>> Get<T>(Expression<Func<Article, bool>> predictate, Func<Article, T> selector);
    public Task<Result<Article>> GetAsync(Guid Id);
}
