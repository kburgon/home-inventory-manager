namespace HomeInventoryManager.Data.Repositories;

public interface IGetsAllRepository<TEntity>
{
	List<TEntity> GetAll();
}
