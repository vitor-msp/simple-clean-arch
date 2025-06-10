using System.Data;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Infra;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application.UseCases;

public class CreateProduct(IProductRepository repository, IMailGateway mail, ITransactionBuilder transactionBuilder) : ICreateProduct
{
    private readonly IProductRepository _repository = repository;
    private readonly IMailGateway _mail = mail;
    private readonly ITransactionBuilder _transactionBuilder = transactionBuilder;

    public async Task<CreateProductOutput> Execute(CreateProductInput input)
    {
        await using var transaction = await _transactionBuilder.BeginTransaction(IsolationLevel.Serializable);
        var savedProduct = await _repository.GetByName(input.Name);
        if (savedProduct is not null)
            throw new ConflictException($"Product with name {input.Name} already exists.");
        var product = input.GetEntity();
        var productId = await _repository.Create(product);
        await transaction.Commit();
        await _mail.SendMail(new SendMailInput(
            "logs@mysystem.com", "Product Creation", $"Created product id {productId}."
        ));
        return new() { ProductId = productId };
    }
}