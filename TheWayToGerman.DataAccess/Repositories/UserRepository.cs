
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
        return await Task.Run(() => PostgresDBContext.Set<User>().FirstOrDefault(Where));// to run it in a background task without blocking the thread
    }

    public async Task<Result<Guid>> AddUserAsync(User user)
    {
        if (user.Password.Length==0)
        {
            return new ArgumentNullException("Password is empty");   
        }
        var users = PostgresDBContext.Set<User>().Where(x => x.Email == user.Email);
      
        if(users.Count() > 0)
        {
            return new UniqueFieldException("Email is Used");
        }
        
        if (users.Where(x=>x.Username==user.Username).Count()>0)
        {
            return new UniqueFieldException("User Name is Used");
        }
        await PostgresDBContext.Set<User>().AddAsync(user);
        return user.Id;
    }
}
