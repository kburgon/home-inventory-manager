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
		if (apiCaller == null)
		{
			throw new ArgumentNullException($"Cannot accept null parameter {nameof(apiCaller)}.");
		}

		_apiCaller = apiCaller;
	}

    public async Task<InventoryItem> Handle(AddInventoryItemWorkflowRequest request, CancellationToken cancellationToken)
    {
		InventoryItem? item = await _apiCaller.GetInventoryItemAsync(request.InventoryItemId);

		if (item == null)
		{
			throw new ArgumentException($"InventoryItem with ID {request.InventoryItemId} does not exist.");
		}

		item.CountInStock = await _apiCaller.UpdateStockAsync(item.InventoryItemId, request.AmountToAdd);
        return item;
    }
}
