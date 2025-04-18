namespace SimpleCleanArch.Application.Contract;

public interface IDeleteWarehouse
{
    Task Execute(Guid id);
}