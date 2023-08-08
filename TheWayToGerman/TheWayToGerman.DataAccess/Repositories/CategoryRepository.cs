﻿using Core.DataKit;
using Core.DataKit.Exceptions;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
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

    public async Task<Result<bool>> IsExistAsync(Func<Category, bool> predictate)
    {
        return await Task.Run<Result<bool>>(() =>
        {
            try
            {
                return PostgresDBContext.Categories.Any(predictate);
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
            var IsExistResult = await IsExistAsync((cate) =>
                cate.Name == category.Name &&
                cate.Language == category.Language
            );

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


    public Result<IEnumerable<T>> Get<T>(Expression<Func<Category, bool>> predictate, Func<Category, T> selector)
    {
        try
        {
            return Result.From(PostgresDBContext.Categories.Where(predictate).Select(selector));
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException("failed to get categories ");
        }
    }
}
