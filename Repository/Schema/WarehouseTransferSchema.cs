using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

public class WarehouseTransferSchema : BaseSchema<IWarehouseTransfer>
{
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

    public override IWarehouseTransfer GetEntity()
        => WarehouseTransfer.Rebuild(Id, CreatedAt, SourceWarehouseId, TargetWarehouseId, ProductId, ProductQuantity);
}