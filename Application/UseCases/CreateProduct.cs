using SimpleCleanArch.Domain.Entities;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Contract.Gateways;
using SimpleCleanArch.Application.Dto;

namespace SimpleCleanArch.Application.UseCases;

public class CreateProduct : ICreateProduct
{
    private readonly IProductsRepository _repository;
    private readonly ISendMailGateway _mail;

    public CreateProduct(IProductsRepository repository, ISendMailGateway mail)
    {
        _repository = repository;
        _mail = mail;
    }

    public CreateProductOutput Execute(CreateProductInput input)
    {
        var product = new Product(input.Product);
        _repository.Save(product);
        long productId = product.GetFields().Id;
        _mail.Send($"created product id {productId}");
        return new CreateProductOutput()
        {
            ProductId = productId
        };
    }
}