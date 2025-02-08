using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Contract.Gateways;
using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.UseCases;

public class CreateProduct(IProductsRepository repository, ISendMailGateway mail) : ICreateProduct
{
    private readonly IProductsRepository _repository = repository;
    private readonly ISendMailGateway _mail = mail;

    public async Task<CreateProductOutput> Execute(CreateProductInput input)
    {
        var product = input.GetEntity();
        await _repository.Create(product);
        await _repository.Commit();
        await _mail.Send($"created product id {product.Id}");
        return new() { ProductId = product.Id };
    }
}