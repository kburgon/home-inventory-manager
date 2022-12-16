using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Workflows.Requests;
using MediatR;

namespace HomeInventoryManager.InventoryManager.Workflows;

public class AddInventoryItemWorkflow : IRequestHandler<AddInventoryItemWorkflowRequest, InventoryItem>
{
    public Task<InventoryItem> Handle(AddInventoryItemWorkflowRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(request.InventoryItem);
    }
}
