using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Application.GetProduct;

public class GetProduct(IProductsRepository repository) : IGetProduct
{
    private readonly IProductsRepository _repository = repository;

    public async Task<GetProductOutput> Execute(long id)
    {
        var product = await _repository.Get(id)
            ?? throw new NotFoundException($"Product id {id} not found.");
        return GetProductOutput.FromEntity(product);
    }
}