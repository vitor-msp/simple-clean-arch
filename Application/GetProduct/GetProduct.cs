using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Application.GetProduct;

public class GetProduct(IProductsRepository repository) : IGetProduct
{
    private readonly IProductsRepository _repository = repository;

    public async Task<GetProductOutput?> Execute(long id)
    {
        var product = await _repository.Get(id);
        if (product is null) return null;
        return GetProductOutput.FromEntity(product);
    }
}