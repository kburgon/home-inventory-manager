using AutoFixture;
using HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;
using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Workflows;
using HomeInventoryManager.InventoryManager.Workflows.Requests;
using Moq;

namespace HomeInventoryManager.InventoryManager.Tests.BusinessActions.Workflows;

public class AddInventoryItemWorkflowTests
{
	[Fact]
	public async Task Handle_DoesIncrementInventory()
	{
		Fixture fixture = new();
		AddInventoryItemWorkflowRequest request = fixture.Create<AddInventoryItemWorkflowRequest>();
		InventoryItem item = fixture.Create<InventoryItem>();
		int initialCount = item.CountInStock;
		IInventoryItemApiCaller apiCaller = Mock.Of<IInventoryItemApiCaller>();
		Mock.Get(apiCaller).Setup(m => m.GetInventoryItem(It.IsAny<int>()))
							.Returns(item);

		AddInventoryItemWorkflow sut = new(apiCaller);

		var response = await sut.Handle(request, new CancellationToken());

		Assert.Equal(initialCount + request.AmountToAdd, response.CountInStock);
	}
}
