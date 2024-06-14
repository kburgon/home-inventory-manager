using HomeInventoryManager.Data.Entities;
using HomeInventoryManager.Data.Repositories;
using MediatR;

namespace HomeInventoryManager.Commands;

public class GetProductsQuery : IRequest<List<Product>>
{
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
{
    private readonly IProductRepository _repo;

    public GetProductsQueryHandler(IProductRepository repo)
	{
		_repo = repo;
	}

	public async Task<List<Product>> Handle(GetProductsQuery query, CancellationToken token)
	{
		return await _repo.GetAllAsync(token);
	}
}
