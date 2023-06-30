
using Core.DataKit;
using Core.DataKit.Exceptions;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Loggers;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public PostgresDBContext PostgresDBContext { get; }
    public IDateTimeProvider DateTimeProvider { get; }
    public ILog Log { get; }

    public CategoryRepository(PostgresDBContext postgresDBContext, IDateTimeProvider dateTimeProvider, ILog log)
    {
        PostgresDBContext = postgresDBContext;
        DateTimeProvider = dateTimeProvider;
        Log = log;
    }

    public async Task<Result<bool>> IsExistAsync(Func<Category, bool> where)
    {
        return await Task.Run<Result<bool>>(() => {
            try
            {
                return  PostgresDBContext.Categories.Any(where);                
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return new InternalErrorException("failed to check if Category exist");
            }         
        });
    }

    public async Task<Result<OK>> AddAsync(Category category)
    {
        try
        {
            var IsExistResult = await IsExistAsync((cate) => cate.Name == category.Name && cate.Language.LanguageName == category.Language.LanguageName);
            if (IsExistResult.ContainData() && IsExistResult.GetData())
            {
                return new UniqueFieldException("category already exist");
            }
            await PostgresDBContext.Categories.AddAsync(category);
            return new OK();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException("failed to add new category ");
        }
    }
}
