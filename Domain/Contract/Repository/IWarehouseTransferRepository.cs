namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IWarehouseTransferRepository : IBaseRepository
{
    Task<int> Create(IWarehouseTransfer warehouseTransfer);
}