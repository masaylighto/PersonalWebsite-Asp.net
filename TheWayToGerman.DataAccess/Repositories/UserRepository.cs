
using Core.DataKit.Result;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.DataAccess.Repositories;

public class UserRepository : IUserRespository
{
    public PostgresDBContext PostgresDBContext { get; }
    public UserRepository(PostgresDBContext postgresDBContext)
    {
        PostgresDBContext = postgresDBContext;
    }

    public async Task<Result<User>> GetUserAsync(Func<User, bool> Where)
    {
        return await Task.Run(() => PostgresDBContext.Users.FirstOrDefault(Where));// to run it in a background task without blocking the thread
    }

    public async Task<Result<Guid>> AddUserAsync(User user)
    {
       
        if (user.IsPasswordNullOrEmpty())
        {
            return new ArgumentNullException("User Password is empty");   
        }    
        if(PostgresDBContext.Users.Any(x => x.Email == user.Email))
        {
            return new UniqueFieldException("Email is Used");
        }
        if (PostgresDBContext.Users.Any(x=>x.Username==user.Username))
        {
            return new UniqueFieldException("User Name is Used");
        }
        await PostgresDBContext.Users.AddAsync(user);
        return user.Id;
    }
}
