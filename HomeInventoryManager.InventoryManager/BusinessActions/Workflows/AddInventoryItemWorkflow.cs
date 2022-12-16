using HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;
using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Workflows.Requests;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows;

public class AddInventoryItemWorkflow : IRequestHandler<AddInventoryItemWorkflowRequest, InventoryItem>
{
	private readonly IInventoryItemApiCaller _apiCaller;

	public AddInventoryItemWorkflow(IInventoryItemApiCaller apiCaller)
	{
		_apiCaller = apiCaller;
	}

    public Task<InventoryItem> Handle(AddInventoryItemWorkflowRequest request, CancellationToken cancellationToken)
    {
		InventoryItem? item = _apiCaller?.GetInventoryItem(request.InventoryItemId);

		if (item == null)
		{
			throw new ArgumentException($"InventoryItem with ID {request.InventoryItemId} does not exist.");
		}

		item.CountInStock += request.AmountToAdd;
        return Task.FromResult(item);
    }
}
