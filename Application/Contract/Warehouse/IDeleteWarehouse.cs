namespace SimpleCleanArch.Application.Contract;

public interface IDeleteWarehouse
{
    Task Execute(int id);
}