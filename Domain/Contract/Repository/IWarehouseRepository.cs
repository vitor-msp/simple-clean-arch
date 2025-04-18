namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IWarehouseRepository
{
    Task<IWarehouse?> GetById(Guid id);
    Task<IWarehouse?> GetByName(string name);
    Task Create(IWarehouse warehouse);
    Task Delete(IWarehouse warehouse);
    Task Update(IWarehouse warehouse);
    Task Commit();
}