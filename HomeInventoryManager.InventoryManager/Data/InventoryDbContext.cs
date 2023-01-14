using HomeInventoryManager.InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeInventoryManager.InventoryManager.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductItem> ProductItems { get; set; }

    public DbSet<InventoryTransaction> Inventorytransactions { get; set; }
}
