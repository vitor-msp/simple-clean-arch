namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IInventoryRepository : IBaseRepository
{
    Task<int> Create(IInventory inventory);
}