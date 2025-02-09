using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application;

public class GetProduct(IProductRepository repository) : IGetProduct
{
    private readonly IProductRepository _repository = repository;

    public async Task<GetProductOutput> Execute(long id)
    {
        var product = await _repository.Get(id)
            ?? throw new NotFoundException($"Product id {id} not found.");
        return GetProductOutput.FromEntity(product);
    }
}