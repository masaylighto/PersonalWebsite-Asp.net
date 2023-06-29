

using Core.DataKit.MockWrapper;
using Core.Expressions;
using Core.LinqExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
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
    public DbSet<Language> Languages { get; set; }
    public DbSet<Category> Categories { get; set; }

    /*-----------------------------Configuration--------------------------------*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LanguageTableConfiguration());
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
    public override int SaveChanges()
    {
        SoftDelete();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SoftDelete();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SoftDelete();
        return base.SaveChangesAsync(cancellationToken);
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SoftDelete();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    void SoftDelete()
    {
        ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted)
            .Apply(entity =>
            {
                entity.State = EntityState.Modified;
                ((BaseEntity)entity.Entity).DeleteDate = DateTime.UtcNow;
            });  
    }

}
