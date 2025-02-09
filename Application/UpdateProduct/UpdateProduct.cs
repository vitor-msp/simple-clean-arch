using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Application.UpdateProduct;

public class UpdateProduct(IProductsRepository repository) : IUpdateProduct
{
    private readonly IProductsRepository _repository = repository;

    public async Task Execute(long id, UpdateProductInput input)
    {
        var product = await _repository.Get(id)
            ?? throw new Exception($"Product id {id} not found.");
        input.Update(product);
        await _repository.Update(product);
        await _repository.Commit();
    }
}