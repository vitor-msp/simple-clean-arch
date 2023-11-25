using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.UseCases;

public class UpdateProduct : IUpdateProduct
{
    private readonly IProductsRepository _repository;

    public UpdateProduct(IProductsRepository repository)
    {
        _repository = repository;
    }

    public UpdateProductOutput? Execute(long id, UpdateProductInput input)
    {
        var product = _repository.Get(id);
        if (product == null) return null;
        product.Update(input.Product.GetFields());
        _repository.Save(product);
        return new UpdateProductOutput()
        {
            ProductId = product.GetFields().Id
        };
    }
}