using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _dbContext;

        public ProductRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products?.ToList() ?? new List<Product>();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}