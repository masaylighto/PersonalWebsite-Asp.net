
using Microsoft.EntityFrameworkCore;
using TheWayToGerman.Core.Database;

namespace UnitTest.DataAccess;

public static class FakeDBContext
{
    public static PostgresDBContext Create()
    {
        var dbContextOptionsBuilder =new  DbContextOptionsBuilder<PostgresDBContext>();
        dbContextOptionsBuilder.UseInMemoryDatabase("UnitTestMemoryDB");
        return new PostgresDBContext(dbContextOptionsBuilder.Options);
    }
}
