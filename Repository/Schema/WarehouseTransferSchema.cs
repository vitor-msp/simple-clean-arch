using System.ComponentModel.DataAnnotations.Schema;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

[Table("warehouse_transfers")]
public class WarehouseTransferSchema : BaseSchema<IWarehouseTransfer, IWarehouseTransfer>
{
    [Column("source_warehouse_id")]
    public Guid SourceWarehouseId { get; set; }

    [Column("target_warehouse_id")]
    public Guid TargetWarehouseId { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("product_quantity")]
    public int ProductQuantity { get; set; }

    public WarehouseTransferSchema() { }

    public WarehouseTransferSchema(IWarehouseTransfer warehouseTransfer)
    {
        Hydrate(warehouseTransfer);
    }

    public override IWarehouseTransfer GetEntity()
        => WarehouseTransfer.Rebuild(Id, CreatedAt, SourceWarehouseId, TargetWarehouseId, ProductId, ProductQuantity);

    public override void Update(IWarehouseTransfer warehouseTransfer)
    {
        Hydrate(warehouseTransfer);
    }

    public void Hydrate(IWarehouseTransfer warehouseTransfer)
    {
        Id = warehouseTransfer.Id;
        CreatedAt = warehouseTransfer.CreatedAt;
        SourceWarehouseId = warehouseTransfer.SourceWarehouseId;
        TargetWarehouseId = warehouseTransfer.TargetWarehouseId;
        ProductId = warehouseTransfer.ProductId;
        ProductQuantity = warehouseTransfer.ProductQuantity;
    }
}