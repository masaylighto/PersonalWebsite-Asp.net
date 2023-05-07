
using Core.DataKit.Result;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
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
}
