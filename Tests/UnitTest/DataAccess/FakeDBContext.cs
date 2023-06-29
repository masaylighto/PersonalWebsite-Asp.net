
using Core.DataKit.MockWrapper;
using Microsoft.EntityFrameworkCore;
using TheWayToGerman.Core.Database;

namespace UnitTest.DataAccess;

public static class FakeDBContext
{
    public static PostgresDBContext Create()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<PostgresDBContext>();
        dbContextOptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        return new PostgresDBContext(dbContextOptionsBuilder.Options);
    }
}
