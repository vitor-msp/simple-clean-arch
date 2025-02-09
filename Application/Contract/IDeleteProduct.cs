namespace SimpleCleanArch.Application.Contract;

public interface IDeleteProduct
{
    Task Execute(long id);
}