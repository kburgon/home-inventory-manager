using HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;
using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Workflows.Requests;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows;

public class DecreaseInventoryItemStockWorkflow : IRequestHandler<DecreaseInventoryItemStockWorkflowRequest, InventoryItem>
{
	private readonly IInventoryItemApiCaller _apiCaller;

	public DecreaseInventoryItemStockWorkflow(IInventoryItemApiCaller apiCaller)
	{
		_apiCaller = apiCaller;
	}

    public async Task<InventoryItem> Handle(DecreaseInventoryItemStockWorkflowRequest request, CancellationToken cancellationToken)
    {
		InventoryItem item = await _apiCaller.GetInventoryItemAsync(request.InventoryItemId);
		if (item == null)
		{
			throw new ArgumentException($"No inventory item found with ID {request.InventoryItemId}.");
		}

		int newStockAmount = item.CountInStock - request.AmountToSubtract;
		item.CountInStock = await _apiCaller.UpdateStockAsync(item.InventoryItemId, newStockAmount);
		return item;
    }
}

