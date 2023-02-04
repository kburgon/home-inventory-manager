using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Data.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Task<Product> CreateProductAsync(Product productName);
    }
}