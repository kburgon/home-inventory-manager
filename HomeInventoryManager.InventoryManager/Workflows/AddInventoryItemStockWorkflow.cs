using HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;
using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Workflows.Requests;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows;

public class AddInventoryItemWorkflow : IRequestHandler<AddInventoryItemStockWorkflowRequest, InventoryItem>
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

    public async Task<InventoryItem> Handle(AddInventoryItemStockWorkflowRequest request, CancellationToken cancellationToken)
    {
		InventoryItem? item = await _apiCaller.GetInventoryItemAsync(request.InventoryItemId);

		if (item == null)
		{
			throw new ArgumentException($"InventoryItem with ID {request.InventoryItemId} does not exist.");
		}

		int newStockAmount = item.CountInStock + request.AmountToAdd;
		item.CountInStock = await _apiCaller.UpdateStockAsync(item.InventoryItemId, newStockAmount);
        return item;
    }
}
