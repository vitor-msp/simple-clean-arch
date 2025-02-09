using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Application.Contract;
using System.Text.Json;

namespace SimpleCleanArch.Application.DeleteProduct;

public class DeleteProduct(IProductsRepository repository, ISendMailGateway mail) : IDeleteProduct
{
    private readonly IProductsRepository _repository = repository;
    private readonly ISendMailGateway _mail = mail;

    public async Task Execute(long id)
    {
        var product = await _repository.Get(id)
            ?? throw new Exception($"product id {id} not found");
        await _mail.Send(JsonSerializer.Serialize(product));
        await _repository.Delete(product);
        await _repository.Commit();
    }
}