
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

public class UserRepository : IUserRepository
{
    public PostgresDBContext PostgresDBContext { get; }
    public IDateTimeProvider DateTimeProvider { get; }
    public ILog Log { get; }

    public UserRepository(PostgresDBContext postgresDBContext, IDateTimeProvider dateTimeProvider, ILog log)
    {
        PostgresDBContext = postgresDBContext;
        DateTimeProvider = dateTimeProvider;
        Log = log;
    }

    public async Task<Result<User>> GetAsync(Func<User, bool> predictate)
    {
        try
        {
            var result = PostgresDBContext.Users.AsEnumerable().FirstOrDefault(predictate);
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

    public async Task<Result<Guid>> AddUserAsync(User user)
    {
        try
        {
            if (user.IsPasswordNullOrEmpty())
            {
                return new NullValueException("User Password is empty");
            }
            if (await PostgresDBContext.Users.AnyAsync(x => x.Email == user.Email))
            {
                return new UniqueFieldException("Email is Used");
            }
            if (await PostgresDBContext.Users.AnyAsync(x => x.Username == user.Username))
            {
                return new UniqueFieldException("User Name is Used");
            }
            await PostgresDBContext.Users.AddAsync(user);
            return user.Id;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : failed to add User");
        }
    }

    public async Task<Result<OK>> UpdateUserAsync(User user, Func<User, bool> predictate)
    {
        try
        {
            var oldEntity = PostgresDBContext.Users.SingleOrDefault(predictate);
            if (oldEntity is null)
            {
                return new NullValueException("Updating User Error: Can't retrieve old user information from db");
            }
            if (oldEntity.Email != user.Email && await PostgresDBContext.Users.AnyAsync(x => x.Email == user.Email))
            {
                return new UniqueFieldException("Email is Used");
            }
            if (oldEntity.Username != user.Username && await PostgresDBContext.Users.AnyAsync(x => x.Username == user.Username))
            {
                return new UniqueFieldException("User Name is Used");
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

    public Result<IAsyncEnumerable<T>> GetUsers<T>(Expression<Func<User, bool>> predictate, Func<User, T> selector, int pageSize, int pageNumber)
    {
        try
        {
          
            var result = PostgresDBContext.Users.Where(predictate).Page(pageSize, pageNumber).Select(selector);
            return Result<IAsyncEnumerable<T>>.From(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : failed to Get Users info");
        }
    }

    public async Task<Result<OK>> DeleteAdminById(Guid Id)
    {
        try
        {
            var user = await PostgresDBContext.Users.FirstOrDefaultAsync(x => x.Id == Id && x.UserType == Core.Enums.UserType.Admin);
            if (user is null)
            {
                return new DataNotFoundException();
            }
            PostgresDBContext.Users.Remove(user);
            return new OK();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : failed to Delete Admin");
        }
    }

    public async Task<Result<OK>> IsUserExistAsync(Expression<Func<User, bool>> predictate)
    {
        try
        {
            if (await PostgresDBContext.Users.AnyAsync(predictate))
            {
                return new OK();
            }
            return new DataNotFoundException();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return new InternalErrorException(ex, "Error on our side : Failed To check if user exist");
        }
    }
}
