namespace HomeInventoryManager.Data.Repositories;

public interface IGetsAllRepository<TEntity>
{
	Task<List<TEntity>> GetAllAsync(CancellationToken token);
}
