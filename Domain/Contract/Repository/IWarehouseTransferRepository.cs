namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IWarehouseTransferRepository: IBaseRepository
{
    Task Create(IWarehouseTransfer warehouseTransfer);
}