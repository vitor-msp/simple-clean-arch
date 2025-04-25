using System.Text.Json;
using SimpleCleanArch.Application.Contract;
using SimpleCleanArch.Application.Exceptions;
using SimpleCleanArch.Domain.Contract.Infra;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Application.UseCases;

public class DeleteProduct(IProductRepository repository, IMailGateway mail) : IDeleteProduct
{
    private readonly IProductRepository _repository = repository;
    private readonly IMailGateway _mail = mail;

    public async Task Execute(int id)
    {
        var product = await _repository.GetById(id)
            ?? throw new NotFoundException($"Product id {id} not found.");
        await _mail.SendMail(new SendMailInput(
            "logs@mysystem.com", "Product Deletion", $"Product id {id} was deleted."
        ));
        await _repository.Delete(product);
        await _repository.Commit();
    }
}