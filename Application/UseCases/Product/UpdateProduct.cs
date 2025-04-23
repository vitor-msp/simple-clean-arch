using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application.UseCases;

public class UpdateProduct(IProductRepository repository) : IUpdateProduct
{
    private readonly IProductRepository _repository = repository;

    public async Task Execute(int id, UpdateProductInput input)
    {
        var product = await _repository.GetById(id)
            ?? throw new NotFoundException($"Product id {id} not found.");
        input.Update(product);
        await _repository.Update(product);
        await _repository.Commit();
    }
}