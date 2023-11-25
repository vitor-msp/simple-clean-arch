using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Dto;
using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Application.UseCases;

public class GetProduct : IGetProduct
{
    private readonly IProductsRepository _repository;

    public GetProduct(IProductsRepository repository)
    {
        _repository = repository;
    }

    public GetProductOutput? Execute(long id)
    {
        var product = _repository.Get(id);
        if(product == null) return null;
        return new GetProductOutput()
        {
            Product = product.GetFields()
        };
    }
}