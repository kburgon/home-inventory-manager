using HomeInventoryManager.Data.Entities;

namespace HomeInventoryManager.Data.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<List<Product>> GetAllAsync(CancellationToken token)
    {
		return Task.FromResult(new List<Product>
		{
			new() { ProductId = 0, ProductName = "Test", Count = 7 },
			new() { ProductId = 0, ProductName = "Test", Count = 7 },
			new() { ProductId = 0, ProductName = "Test", Count = 7 },
			new() { ProductId = 0, ProductName = "Test", Count = 7 },
			new() { ProductId = 0, ProductName = "Test", Count = 7 }
		});
    }
}

