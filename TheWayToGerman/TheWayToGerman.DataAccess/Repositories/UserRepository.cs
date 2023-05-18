
using Core.DataKit;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public PostgresDBContext PostgresDBContext { get; }
    public IDateTimeProvider DateTimeProvider { get; }

    public UserRepository(PostgresDBContext postgresDBContext, IDateTimeProvider dateTimeProvider)
    {
        PostgresDBContext = postgresDBContext;
        DateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<User>> GetUserAsync(Func<User, bool> Where)
    {
        return await Task.Run<Result<User>>(() =>
        {
            try
            {
                var result = PostgresDBContext.Users.FirstOrDefault(Where);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        });// to run it in a background task without blocking the thread
    }

    public async Task<Result<Guid>> AddUserAsync(User user)
    {
        try
        {
            if (user.IsPasswordNullOrEmpty())
            {
                return new NullValueException("User Password is empty");
            }
            if (PostgresDBContext.Users.Any(x => x.Email == user.Email))
            {
                return new UniqueFieldException("Email is Used");
            }
            if (PostgresDBContext.Users.Any(x => x.Username == user.Username))
            {
                return new UniqueFieldException("User Name is Used");
            }
            await PostgresDBContext.Users.AddAsync(user);
            return user.Id;
        }
        catch (Exception ex)
        {
            return ex;
        }

    }

    public async Task<Result<OK>> UpdateUserAsync(User user, Func<User, bool> Which)
    {

        try
        {
            var oldEntity = PostgresDBContext.Users.SingleOrDefault(Which);
            if (oldEntity is null)
            {
                return new NullValueException("Updating User Error: Can't retrieve old user information from db");
            }
            if (oldEntity.Email != user.Email && PostgresDBContext.Users.Any(x => x.Email == user.Email))
            {
                return new UniqueFieldException("Email is Used");
            }
            if (oldEntity.Username != user.Username && PostgresDBContext.Users.Any(x => x.Username == user.Username))
            {
                return new UniqueFieldException("User Name is Used");
            }
            oldEntity.UpdateFrom(user, DateTimeProvider.UtcNow);
            return new OK();
        }
        catch (Exception ex)
        {
            return ex;
        }

    }

    public async Task<Result<IEnumerable<T>>> GetUsersAsync<T>(Expression<Func<User, bool>> Where, Func<User, T> select)
    {

        try
        {
          
            var result = PostgresDBContext.Users.AsEnumerable().Where(Where.Compile()).Select(select);
            return Result<IEnumerable<T>>.From(result);
        }
        catch (Exception ex)
        {
            return ex;
        }

    }

    public async Task<Result<OK>> DeleteAdminById(Guid Id)
    {
        var user = PostgresDBContext.Users.Where(x => x.Id == Id && x.UserType == Core.Enums.UserType.Admin).FirstOrDefault();
        if (user is null )
        {
            return new DataNotFoundException();
        }
        PostgresDBContext.Users.Remove(user);
        return new OK();
    }
}
