using HomeInventoryManager.Data.Entities;

namespace HomeInventoryManager.Data;

public interface IProductRepository
{
	List<Product> GetAll();
}
