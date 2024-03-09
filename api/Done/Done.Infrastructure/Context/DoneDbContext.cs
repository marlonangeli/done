using System.Reflection;
using Done.Domain.Entities;
using Done.Domain.Shared;
using Done.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Done.Infrastructure.Context;

public class DoneDbContext : DbContext
{
    public DbSet<ToDo> ToDos => Set<ToDo>();
    public DbSet<ToDoList> ToDoLists => Set<ToDoList>();

    public DoneDbContext(DbContextOptions<DoneDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyUtcDateTimeConverter();

        modelBuilder.Ignore<Entity>();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var utcNow = DateTime.Now;

        foreach (var entityEntry in ChangeTracker.Entries<IEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IEntity.CreatedAt)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IEntity.UpdatedAt)).CurrentValue = utcNow;
            }
        }
    }
}