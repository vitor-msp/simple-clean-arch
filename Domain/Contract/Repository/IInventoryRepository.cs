namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IInventoryRepository: IBaseRepository
{
    Task Create(IInventory inventory);
}