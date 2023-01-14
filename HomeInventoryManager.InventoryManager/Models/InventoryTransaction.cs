namespace HomeInventoryManager.InventoryManager.Models;

public class InventoryTransaction
{
    public int InventoryTransactionId { get; set; } = 0;

    public int ProductItemId { get; set; } = 0;

    public int InventoryAdjustment { get; set; } = 0;

    public DateTime AdjustedAt { get; set; } = new();
}