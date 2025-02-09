namespace SimpleCleanArch.Application.DeleteProduct;

public interface IDeleteProduct
{
    Task Execute(long id);
}