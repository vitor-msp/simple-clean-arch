using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Application.Contract.UseCases;
using SimpleCleanArch.Application.Dto;
using SimpleCleanArch.Application.Contract.Gateways;
using System.Text.Json;

namespace SimpleCleanArch.Application.UseCases;

public class DeleteProduct : IDeleteProduct
{
    private readonly IProductsRepository _repository;
    private readonly ISendMailGateway _mail;

    public DeleteProduct(IProductsRepository repository, ISendMailGateway mail)
    {
        _repository = repository;
        _mail = mail;
    }

    public DeleteProductOutput? Execute(long id)
    {
        var product = _repository.Get(id);
        if (product == null) return null;
        var fields = product.GetFields();
        _mail.Send(JsonSerializer.Serialize(fields));
        _repository.Delete(product);
        return new DeleteProductOutput()
        {
            ProductId = fields.Id
        };
    }
}