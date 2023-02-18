using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Data.Repositories;

public interface IInventoryTransactionRepository
{
    List<InventoryTransaction> GetAll();

    List<InventoryTransaction> GetAllByProductItemId(int productItemId);   

    Task<InventoryTransaction> CreateAsync(InventoryTransaction transaction);
}