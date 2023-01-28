using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Services
{
    public interface IInventoryManagementService
    {
        IEnumerable<Product> GetProducts();
    }
}