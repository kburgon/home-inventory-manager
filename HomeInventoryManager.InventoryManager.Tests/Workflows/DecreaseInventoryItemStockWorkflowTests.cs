using AutoFixture;
using HomeInventoryManager.InventoryManager.BusinessActions.DataManagement;
using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Workflows;
using HomeInventoryManager.InventoryManager.Workflows.Requests;
using Moq;

namespace HomeInventoryManager.InventoryManager.Tests.Workflows;

public class DecreaseInventoryItemStockWorkflowTests
{
	[Fact]
	public async Task Handle_DoesDecrementStock()
	{
		Fixture fixture = new();
		var request = fixture.Create<DecreaseInventoryItemStockWorkflowRequest>();
		var item = fixture.Create<InventoryItem>();
		int originalStock = item.CountInStock;

		Mock<IInventoryItemApiCaller> apiCaller = new();
		apiCaller.Setup(m => m.GetInventoryItemAsync(It.IsAny<int>()))
				.ReturnsAsync(item);
		apiCaller.Setup(m => m.UpdateStockAsync(It.IsAny<int>(), It.IsAny<int>()))
				.ReturnsAsync(item.CountInStock - request.AmountToSubtract);

		DecreaseInventoryItemStockWorkflow sut = new(apiCaller.Object);

		var response = await sut.Handle(request, new CancellationToken());

		// Note here that the 2nd required parameter is asking for the item's stock count.
		// Because item is a referenced value here, the new CountInStock will be the stock after subtraction if the sobtraction operation is in the workflow logic.
		apiCaller.Verify(m => m.UpdateStockAsync(
						It.Is<int>(i => i.Equals(item.InventoryItemId)),
						It.Is<int>(i => i.Equals(item.CountInStock))
					), Times.Once);
		Assert.Equal(originalStock - request.AmountToSubtract, item.CountInStock);
	}
}
