

using Core.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.Core.Database;

public class PostgresDBContext : DbContext
{
    public PostgresDBContext(DbContextOptions<PostgresDBContext> dbContextOptions) : base(dbContextOptions)
    {
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
    }
    public DbSet<User> Users { get; set; }
    /*-----------------------------Configuration--------------------------------*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
        Expression<Func<BaseEntity, bool>> SoftDelete = X => X.DeleteDate == null;
        var entites = modelBuilder.Model.GetEntityTypes().Where(x => x.ClrType.IsAssignableTo(typeof(BaseEntity)));
        foreach (var entityType in entites)
        {
            // modify expression to handle correct child type               
            var rebinded = SoftDelete.RebindBodyParamFrom(entityType.ClrType).BodyToLambda();
            entityType.SetQueryFilter(rebinded);

        }
        base.OnModelCreating(modelBuilder);
    }
    private EntityEntry<T> SoftDelete<T>(object baseEntity) where T : class
    {

        ((BaseEntity)baseEntity).DeleteDate = DateTime.Now;
        var entityEntry = base.Remove((T)baseEntity);
        entityEntry.State = EntityState.Modified; // so it will not be deleted
        return entityEntry;

    }
    public override EntityEntry Remove(object entity)
    {
        return SoftDelete<object>(entity);
    }
    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        return SoftDelete<TEntity>(entity);
    }
    public override void RemoveRange(IEnumerable<object> entities)
    {
        foreach (var entity in entities)
        {
            SoftDelete<object>(entity);
        }

    }
    public override void RemoveRange(params object[] entities)
    {
        foreach (var entity in entities)
        {
            SoftDelete<object>(entity);
        }
    }
}
