using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Repository.Schema;

public class WarehouseTransferSchema
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid SourceWarehouseId { get; set; }
    public Guid TargetWarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductQuantity { get; set; }

    public WarehouseTransferSchema() { }

    public WarehouseTransferSchema(IWarehouseTransfer warehouseTransfer)
    {
        Id = warehouseTransfer.Id;
        CreatedAt = warehouseTransfer.CreatedAt;
        SourceWarehouseId = warehouseTransfer.SourceWarehouseId;
        TargetWarehouseId = warehouseTransfer.TargetWarehouseId;
        ProductId = warehouseTransfer.ProductId;
        ProductQuantity = warehouseTransfer.ProductQuantity;
    }
}