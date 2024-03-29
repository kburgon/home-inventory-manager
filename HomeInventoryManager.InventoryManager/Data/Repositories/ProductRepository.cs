using HomeInventoryManager.InventoryManager.Models;
using Microsoft.EntityFrameworkCore;

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
            => _dbContext.Products?
                         .Include(p => p.ProductItems)
                         .ToList() ?? new List<Product>();

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public Product GetProductById(int productId) 
            => _dbContext.Products?
                         .Include(p => p.ProductItems)
                         .SingleOrDefault(p => p.ProductId == productId) ?? new Product();
    }
}