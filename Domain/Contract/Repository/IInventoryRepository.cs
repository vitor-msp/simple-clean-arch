namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IInventoryRepository
{
    Task Create(IInventory inventory);
    Task Commit();
}