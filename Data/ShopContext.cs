using Microsoft.EntityFrameworkCore;
using Storefront.Models;

namespace Storefront.Data;

public class ShopContext
: DbContext
{
  public ShopContext
  (DbContextOptions<ShopContext> options)
  : base(options)
  {
  }

  public DbSet<Product> Products { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<ShippingInfo> ShippingInfos { get; set; }
  public DbSet<Category> Categories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Product>()
        .Property(p => p.Audience)
        .HasConversion<string>();

    modelBuilder.Entity<Product>()
        .Property(p => p.BaseColour)
        .HasConversion<string>();

    modelBuilder.Entity<Product>()
        .Property(p => p.Condition)
        .HasConversion<string>();
    
  }

    // ... your DbSets and OnModelCreating configuration ...

    public override int SaveChanges()
    {
        ApplyAuditTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditable && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var auditableEntity = (IAuditable)entry.Entity;
            var now = DateTimeOffset.UtcNow;

            if (entry.State == EntityState.Added)
            {
                auditableEntity.CreatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                auditableEntity.UpdatedAt = now;
            }
        }

    }

}