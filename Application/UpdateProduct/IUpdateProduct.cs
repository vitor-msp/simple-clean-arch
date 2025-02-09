namespace SimpleCleanArch.Application.UpdateProduct;

public interface IUpdateProduct
{
    Task Execute(long id, UpdateProductInput input);
}