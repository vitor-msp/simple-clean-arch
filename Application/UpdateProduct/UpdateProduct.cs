using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.UseCases;

public class UpdateProduct(IProductsRepository repository) : IUpdateProduct
{
    private readonly IProductsRepository _repository = repository;

    public async Task<UpdateProductOutput?> Execute(long id, UpdateProductInput input)
    {
        var product = await _repository.Get(id);
        if (product is null) return null;
        input.Update(product);
        await _repository.Update(product);
        await _repository.Commit();
        return new() { ProductId = product.Id };
    }
}