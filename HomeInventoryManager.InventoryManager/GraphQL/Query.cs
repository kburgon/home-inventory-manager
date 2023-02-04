using HomeInventoryManager.InventoryManager.Data.Repositories;
using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.GraphQL
{
    public class Query
    {
        public List<Product> GetAllProducts([Service] ProductRepository productRepository)
            => productRepository.GetProducts();
    }
}