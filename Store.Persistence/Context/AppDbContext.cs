using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Application.Interface.AppDbContextInterfaces;
using Store.Domain.Attributes;
using Store.Domain.Entities.User;

namespace Store.Persistence.Context;

public class AppDbContext : DbContext,IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {

        // builder.Entity<User>().Property<DateTime>("InsertDate");
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
            {
                builder.Entity(entityType.Name).Property<DateTime>("InsertedDate");
                builder.Entity(entityType.Name).Property<DateTime?>("UpdatedDate");
                builder.Entity(entityType.Name).Property<DateTime>("DeletedDate");
                builder.Entity(entityType.Name).Property<bool>("IsDelete");
            }
        }
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Added ||
                        p.State == EntityState.Modified ||
                        p.State == EntityState.Deleted);

        foreach (EntityEntry item in modifiedEntries)
        {
            var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
            var inserted = entityType.FindProperty("InsertedDate");
            var updated = entityType.FindProperty("UpdatedDate");
            var removeTime = entityType.FindProperty("DeletedDate");
            var isDelete = entityType.FindProperty("IsDelete");
            if (item.State == EntityState.Added && inserted != null)
            {
                item.Property("InsertedDate").CurrentValue = DateTime.Now;
            }
            if (item.State == EntityState.Modified && updated != null)
            {
                item.Property("UpdatedDate").CurrentValue = DateTime.Now;
            }
            if (item.State == EntityState.Deleted && removeTime != null && isDelete != null)
            {
                item.Property("DeletedDate").CurrentValue = DateTime.Now;
                item.Property("IsDelete").CurrentValue = true;
            }
        }
        
        return base.SaveChanges();
    }
}