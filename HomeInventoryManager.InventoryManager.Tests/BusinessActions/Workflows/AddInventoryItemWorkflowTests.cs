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
		IInventoryItemApiCaller apiCaller = Mock.Of<IInventoryItemApiCaller>();
		Mock.Get(apiCaller).Setup(m => m.GetInventoryItemAsync(It.IsAny<int>()))
							.ReturnsAsync(item);
		Mock.Get(apiCaller)
			.Setup(m => m.UpdateStockAsync(It.IsAny<int>(), It.IsAny<int>()))
			.ReturnsAsync(item.CountInStock + request.AmountToAdd);

		AddInventoryItemWorkflow sut = new(apiCaller);

		var response = await sut.Handle(request, new CancellationToken());

		Mock.Get(apiCaller)
			.Verify(m => m.UpdateStockAsync(
					It.Is<int>(i => i.Equals(item.InventoryItemId)),
					It.Is<int>(i => i.Equals(request.AmountToAdd))
				), Times.Once);
	}
}
