namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IWarehouseRepository
{
    Task<IWarehouse?> GetById(Guid id);
    Task<IWarehouse?> GetByName(string name);
    Task Create(IWarehouse product);
    Task Delete(IWarehouse product);
    Task Commit();
}