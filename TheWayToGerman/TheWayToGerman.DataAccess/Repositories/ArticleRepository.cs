using Core.DataKit;
using Core.DataKit.Exceptions;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;
using Core.LinqExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Loggers;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

public class ArticleRepository : IArticleRepository
{
    public PostgresDBContext PostgresDBContext { get; }
    public IDateTimeProvider DateTimeProvider { get; }
    public ILog Log { get; }
    public ArticleRepository(PostgresDBContext postgresDBContext, IDateTimeProvider dateTimeProvider, ILog log)
    {
        PostgresDBContext = postgresDBContext;
        DateTimeProvider = dateTimeProvider;
        Log = log;
    }
    public async Task<State> AddAsync(Article article)
    {
        try
        {            
            await PostgresDBContext.Articles.AddAsync(article);
            return new OK();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException("failed to insert new article");
        }
    }

    public Result<IAsyncEnumerable<T>> GetAsync<T>(Expression<Func<Article, bool>> predictate, Func<Article, T> selector)
    {
        try
        {
            return Result.From(PostgresDBContext.Articles.Where(predictate).Select(selector));
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException("failed to get Articles ");
        }
    }

    public async Task<Result<Article>> GetAsync(Guid Id)
    {
        try
        {
            return await PostgresDBContext.Articles.FirstAsync(x => x.Id == Id);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException("failed to retrieve Article");
        }

    }

    public async Task<Result<bool>> IsExistAsync(Func<Article, bool> predictate)
    {
        return await Task.Run<Result<bool>>(() =>
        {
            try
            {
                return PostgresDBContext.Articles.Include(x=>x.Category).Any(predictate);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return new InternalErrorException("failed to check if Article exist");
            }
        });
    }

    public State SetStateToUpdate(Article article)
    {      
        try
        {
                PostgresDBContext.Entry<Article>(article).State = EntityState.Modified;
                return new OK();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException("update article failed");
        }      
    }
}
