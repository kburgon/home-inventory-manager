using HomeInventoryManager.InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeInventoryManager.InventoryManager.Data.Repositories;

public class ProductItemRepository : IProductItemRepository
{
    private readonly InventoryDbContext _dbContext;

    public ProductItemRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ProductItem> GetAll() 
        => _dbContext?.ProductItems?
            .Include(p => p.Product)
            .ToList() ?? new List<ProductItem>();

    public List<ProductItem> GetByProductId(int productId) 
        => _dbContext?.ProductItems?.Where(pi => pi.ProductId.Equals(productId))
                                    .Include(pi => pi.Product)
                                    .ToList() ?? new List<ProductItem>();

    public async Task<ProductItem> CreateAsync(ProductItem item)
    {
        await _dbContext.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public List<ProductItem> GetByItemBarcodeNumber(string itemBarcodeNumber) 
        => _dbContext?.ProductItems?.Where(pi => pi.ItemBarcodeNumber.Equals(itemBarcodeNumber))
                                    .Include(pi => pi.Product)
                                    .ToList() ?? new List<ProductItem>();

    public ProductItem GetByProductItemId(int productItemId)
        => _dbContext?.ProductItems?.Where(pi => pi.ProductItemId.Equals(productItemId))
                                    .Include(pi => pi.Product)
                                    .FirstOrDefault() ?? new ProductItem();
}