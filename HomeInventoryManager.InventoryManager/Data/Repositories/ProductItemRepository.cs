using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Data.Repositories;

public class ProductItemRepository : IProductItemRepository
{
    private readonly InventoryDbContext _dbContext;

    public ProductItemRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ProductItem> GetAll() 
        => _dbContext?.ProductItems?.ToList()
           ?? new List<ProductItem>();

    public List<ProductItem> GetByProductId(int productId) 
        => _dbContext?.ProductItems?.Where(pi => pi.ProductId.Equals(productId))
                                    .ToList() ?? new List<ProductItem>();

    public async Task<ProductItem> CreateAsync(ProductItem item)
    {
        await _dbContext.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}