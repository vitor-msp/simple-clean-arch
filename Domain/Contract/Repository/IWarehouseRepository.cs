namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IWarehouseRepository
{
    Task<IWarehouse?> GetByName(string name);
    Task Create(IWarehouse product);
    Task Commit();
}