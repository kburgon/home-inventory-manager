using HomeInventoryManager.InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeInventoryManager.InventoryManager.Data.Repositories;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private readonly InventoryDbContext _dbContext;

    public InventoryTransactionRepository(InventoryDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<InventoryTransaction> CreateAsync(InventoryTransaction transaction)
    {
        var matchingTransaction = _dbContext.Inventorytransactions?
                                    .FirstOrDefault(it => it.ProductItemId == transaction.ProductItemId
                                                          && it.InventoryAdjustment == transaction.InventoryAdjustment
                                                          && it.AdjustedAt == transaction.AdjustedAt);
        if (matchingTransaction != default(InventoryTransaction))
        {
            return matchingTransaction;
        }

        await _dbContext.AddAsync(transaction);
        await _dbContext.SaveChangesAsync();
        return transaction;
    }

    public List<InventoryTransaction> GetAll()
        => _dbContext.Inventorytransactions?
                     .Include(it => it.ProductItem)
                     .Include(it => it.ProductItem.Product)
                     .ToList() ?? new List<InventoryTransaction>();

    public List<InventoryTransaction> GetAllByProductItemId(int productItemId)
        => _dbContext.Inventorytransactions?
                     .Include(it => it.ProductItem)
                     .Include(it => it.ProductItem.Product)
                     .Where(it => it.ProductItemId == productItemId)
                     .ToList() ?? new List<InventoryTransaction>();
}
