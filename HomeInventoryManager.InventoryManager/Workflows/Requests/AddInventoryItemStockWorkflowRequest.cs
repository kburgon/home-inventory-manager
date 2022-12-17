using HomeInventoryManager.InventoryManager.Models;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows.Requests;

public class AddInventoryItemStockWorkflowRequest : IRequest<InventoryItem>
{
	public int InventoryItemId { get; set; }

	public int AmountToAdd { get; set; }
}
