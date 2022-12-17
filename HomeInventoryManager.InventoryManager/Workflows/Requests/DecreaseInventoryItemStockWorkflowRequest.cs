using HomeInventoryManager.InventoryManager.Models;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows.Requests;

public class DecreaseInventoryItemStockWorkflowRequest : IRequest<InventoryItem>
{
	public int InventoryItemId { get; set; }

	public int AmountToSubtract { get; set; }
}
