using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;

public interface IInventoryItemApiCaller
{
	InventoryItem GetInventoryItem(int id);
}
