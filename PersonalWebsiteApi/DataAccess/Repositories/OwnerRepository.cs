
using Core.DataKit;
using Core.DataKit.Exceptions;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using Core.LinqExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PersonalWebsiteApi.Core.Database;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.Core.Exceptions;
using PersonalWebsiteApi.Core.Loggers;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.DataAccess.Repositories;

public class OwnerRepository : IOwnerRepository
{
    public PostgresDBContext PostgresDBContext { get; }
    public IDateTimeProvider DateTimeProvider { get; }
    public ILog Log { get; }

    public OwnerRepository(PostgresDBContext postgresDBContext, IDateTimeProvider dateTimeProvider, ILog log)
    {
        PostgresDBContext = postgresDBContext;
        DateTimeProvider = dateTimeProvider;
        Log = log;
    }

    public async Task<Result<User>> GetAsync()
    {
        try
        {
            var result = await PostgresDBContext.Users.FirstOrDefaultAsync();
            if (result is null)
            {
                return new DataNotFoundException("Sorry. Couldn't find user with the provided information.");
            }
            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : failed to get User info");
        }
    }

    public async Task<Result<Guid>> AddAsync(User user)
    {
        try
        {
            if (user.IsPasswordNullOrEmpty())
            {
                return new NullValueException("User Password is empty");
            }
            if (await PostgresDBContext.Users.AnyAsync())
            {
                return new UniqueFieldException("Owner is already created");
            }
            await PostgresDBContext.Users.AddAsync(user);
            return user.Id;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : failed to add Owner");
        }
    }

    public async Task<Result<OK>> UpdateAsync(User user)
    {
        try
        {
            var oldEntity = await PostgresDBContext.Users.SingleOrDefaultAsync();
            if (oldEntity is null)
            {
                return new NullValueException("Updating User Error: Can't retrieve old user information from db");
            }            
            oldEntity.UpdateFrom(user, DateTimeProvider.UtcNow);
            return new OK();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : failed to Update User info");
        }
    }
  
    public async Task<Result<OK>> IsOwnerExistAsync()
    {
        try
        {
            if (await PostgresDBContext.Users.AnyAsync())
            {
                return new OK();
            }
            return new DataNotFoundException();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : Failed To check if Owner Exist");
        }
    }
}
