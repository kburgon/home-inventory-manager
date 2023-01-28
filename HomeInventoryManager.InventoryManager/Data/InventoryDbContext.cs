using HomeInventoryManager.InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeInventoryManager.InventoryManager.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);
    }

    public DbSet<Product>? Products { get; set; }

    public DbSet<ProductItem>? ProductItems { get; set; }

    public DbSet<InventoryTransaction>? Inventorytransactions { get; set; }
}
