namespace SimpleCleanArch.Application.Contract;

public interface IDeleteProduct
{
    Task Execute(Guid id);
}