using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Common;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Persistence.Contexts;

public class ECommerceDbContext : IdentityDbContext<User, Role, string>
{
    public ECommerceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Domain.Entities.File> Files { get; set; }
    public DbSet<ProductImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<CompletedOrder> CompletedOrders { get; set; }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // ChangeTracker : Entityler üzerinde yapılan değişikliklerin yada yeni eklenen verilerin yakalanmasını sağlanan property. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar
        
        IEnumerable<EntityEntry<BaseEntity>> datas = ChangeTracker
            .Entries<BaseEntity>().Where(e =>
                e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var data in datas)
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
            };

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>()
            .HasKey(c => c.Id);

        builder.Entity<Order>()
            .HasIndex(c => c.OrderCode)
            .IsUnique();
        
        builder.Entity<Basket>()
            .HasOne(c => c.Order)
            .WithOne(c => c.Basket)
            .HasForeignKey<Order>(c => c.Id);


        builder.Entity<Order>()
            .HasOne(c => c.CompletedOrder)
            .WithOne(c => c.Order)
            .HasForeignKey<CompletedOrder>(c => c.OrderId);

        base.OnModelCreating(builder);
    }
}