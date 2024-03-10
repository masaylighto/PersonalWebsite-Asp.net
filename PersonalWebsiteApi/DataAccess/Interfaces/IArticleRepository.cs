
using Core.DataKit;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;
using System.Linq.Expressions;
using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.DataAccess.Interfaces;

public interface IArticleRepository
{
    public Task<Result<bool>> IsExistAsync(Func<Article, bool> predictate);
    public Task<State> AddAsync(Article article);
    public State SetStateToUpdate(Article article);
    public Result<IAsyncEnumerable<T>> GetAsync<T>(Expression<Func<Article, bool>> predictate, Func<Article, T> selector,int pageNumber,int pageSize);
    public Task<Result<Article>> GetAsync(Guid Id);
}
