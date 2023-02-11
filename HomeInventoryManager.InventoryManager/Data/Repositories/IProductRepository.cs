using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Data.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();

        Product GetProductById(int productId);

        Task<Product> CreateProductAsync(Product productName);
    }
}