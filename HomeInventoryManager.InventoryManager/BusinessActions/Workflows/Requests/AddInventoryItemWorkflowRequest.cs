using HomeInventoryManager.InventoryManager.Models;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows.Requests;

public class AddInventoryItemWorkflowRequest : IRequest<InventoryItem>
{
	public InventoryItem InventoryItem { get; set; } = new();

	public int AmountToAdd { get; set; }
}
