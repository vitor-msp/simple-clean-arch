using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Infra;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application;

public class CreateProduct(IProductRepository repository, IMailGateway mail) : ICreateProduct
{
    private readonly IProductRepository _repository = repository;
    private readonly IMailGateway _mail = mail;

    public async Task<CreateProductOutput> Execute(CreateProductInput input)
    {
        var savedProduct = await _repository.GetByName(input.Name);
        if (savedProduct is not null)
            throw new ConflictException($"Product with name {input.Name} already exists.");
        var product = input.GetEntity();
        var productId = await _repository.Create(product);
        await _mail.SendMail(new SendMailInput(
            "logs@mysystem.com", "Product Creation", $"Created product id {productId}."
        ));
        return new() { ProductId = productId };
    }
}