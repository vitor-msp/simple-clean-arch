namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IWarehouseRepository : IBaseRepository
{
    Task<IWarehouse?> GetById(int id);
    Task<IWarehouse?> GetByName(string name);
    Task<int> Create(IWarehouse warehouse);
    Task Delete(IWarehouse warehouse);
    Task Update(IWarehouse warehouse);
}