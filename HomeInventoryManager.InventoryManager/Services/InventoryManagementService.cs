using HomeInventoryManager.InventoryManager.Data;
using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Services
{
    public class InventoryManagementService : IInventoryManagementService
    {
        private readonly InventoryDbContext _dbContext;

        public InventoryManagementService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products?.AsEnumerable() ?? new List<Product>();
        }
    }
}