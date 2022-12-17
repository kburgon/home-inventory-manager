using AutoFixture;
using HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;
using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Workflows;
using HomeInventoryManager.InventoryManager.Workflows.Requests;
using Moq;

namespace HomeInventoryManager.InventoryManager.Tests.BusinessActions.Workflows;

public class AddInventoryItemStockWorkflowTests
{
	[Fact]
	public async Task Handle_DoesIncrementInventory()
	{
		Fixture fixture = new();
		var request = fixture.Create<AddInventoryItemStockWorkflowRequest>();
		var item = fixture.Create<InventoryItem>();
		int originalStock = item.CountInStock;

		Mock<IInventoryItemApiCaller> apiCaller = new();
		apiCaller.Setup(m => m.GetInventoryItemAsync(It.IsAny<int>()))
				.ReturnsAsync(item);
		apiCaller.Setup(m => m.UpdateStockAsync(It.IsAny<int>(), It.IsAny<int>()))
				.ReturnsAsync(item.CountInStock + request.AmountToAdd);

		AddInventoryItemWorkflow sut = new(apiCaller.Object);

		var response = await sut.Handle(request, new CancellationToken());

		apiCaller.Verify(m => m.UpdateStockAsync(
					It.Is<int>(i => i.Equals(item.InventoryItemId)),
					It.Is<int>(i => i.Equals(item.CountInStock))
				), Times.Once);
		Assert.Equal(originalStock + request.AmountToAdd, item.CountInStock);
	}
}
