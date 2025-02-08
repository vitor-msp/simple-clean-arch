using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Dto;
using SimpleCleanArch.Application.Contract.Gateways;
using System.Text.Json;

namespace SimpleCleanArch.Application.UseCases;

public class DeleteProduct(IProductsRepository repository, ISendMailGateway mail) : IDeleteProduct
{
    private readonly IProductsRepository _repository = repository;
    private readonly ISendMailGateway _mail = mail;

    public async Task<DeleteProductOutput?> Execute(long id)
    {
        var product = await _repository.Get(id);
        if (product is null) return null;
        await _mail.Send(JsonSerializer.Serialize(product));
        await _repository.Delete(product);
        await _repository.Commit();
        return new() { ProductId = id };
    }
}