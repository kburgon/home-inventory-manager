using HomeInventoryManager.InventoryManager.Models;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows.Requests;

public class AddInventoryItemWorkflowRequest : IRequest<InventoryItem>
{
	public int InventoryItemId { get; set; }

	public int AmountToAdd { get; set; }
}
