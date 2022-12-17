using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;

public interface IInventoryItemApiCaller
{
	Task<InventoryItem> GetInventoryItemAsync(int id);

	Task<int> UpdateStockAsync(int id, int stockCount);
}
