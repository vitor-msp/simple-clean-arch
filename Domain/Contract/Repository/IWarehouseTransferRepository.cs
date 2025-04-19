namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IWarehouseTransferRepository
{
    Task Create(IWarehouseTransfer warehouseTransfer);
    Task Commit();
}