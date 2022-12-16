namespace HomeInventoryManager.InventoryManager.Models;

/// <summary>
/// Represents an item that is kept in inventory.
/// </summary>
public class InventoryItem
{
	public int InventoryItemId { get; set; }

	public string Name { get; set; } = string.Empty;

	public int CountInStock { get; set; }
}
