using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Domain.Contract.Infra;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application;

public class CreateProduct(IProductRepository repository, IMailGateway mail) : ICreateProduct
{
    private readonly IProductRepository _repository = repository;
    private readonly IMailGateway _mail = mail;

    public async Task<CreateProductOutput> Execute(CreateProductInput input)
    {
        var product = input.GetEntity();
        await _repository.Create(product);
        await _repository.Commit();
        await _mail.SendMail(new SendMailInput(
            "logs@mysystem.com", "Product Creation", $"Created product id {product.Id}."
        ));
        return new() { ProductId = product.Id };
    }
}