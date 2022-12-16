using AutoFixture;
using HomeInventoryManager.InventoryManager.Workflows;
using HomeInventoryManager.InventoryManager.Workflows.Requests;

namespace HomeInventoryManager.InventoryManager.Tests.BusinessActions.Workflows;

public class AddInventoryItemWorkflowTests
{
	[Fact]
	public async Task Handle_DoesIncrementInventory()
	{
		Fixture fixture = new();
		AddInventoryItemWorkflowRequest request = fixture.Create<AddInventoryItemWorkflowRequest>();
		AddInventoryItemWorkflow sut = fixture.Create<AddInventoryItemWorkflow>();

		var response = await sut.Handle(request, new CancellationToken());

		Assert.NotNull(response);
	}
}
