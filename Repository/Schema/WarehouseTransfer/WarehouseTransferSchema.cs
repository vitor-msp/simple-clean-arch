using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

[Table("warehouse_transfers")]
public class WarehouseTransferSchema : BaseSchema, IRegenerableSchema<IWarehouseTransfer>
{
    [Key, Column("id")]
    public int Id { get; set; }

    [ForeignKey("SourceWarehouse"), Column("source_warehouse_id")]
    public int SourceWarehouseId { get; set; }

    public WarehouseSchema? SourceWarehouse { get; set; }

    [ForeignKey("TargetWarehouse"), Column("target_warehouse_id")]
    public int TargetWarehouseId { get; set; }

    public WarehouseSchema? TargetWarehouse { get; set; }

    [ForeignKey("Product"), Column("product_id")]
    public int ProductId { get; set; }

    public ProductSchema? Product { get; set; }

    public WarehouseTransferDetailsSchema? Details { get; set; }

    public WarehouseTransferSchema() { }

    public WarehouseTransferSchema(IWarehouseTransfer warehouseTransfer)
    {
        Hydrate(warehouseTransfer);
        Details = new WarehouseTransferDetailsSchema(warehouseTransfer) { WarehouseTransfer = this };
    }

    public IWarehouseTransfer GetEntity()
        => WarehouseTransfer.Rebuild(Id, SourceWarehouseId, TargetWarehouseId, ProductId, Details?.ProductQuantity
            ?? throw new Exception("WarehouseTransferSchema must contain Details to rebuild WarehouseTransfer."));

    private void Hydrate(IWarehouseTransfer warehouseTransfer)
    {
        Id = warehouseTransfer.Id;
        SourceWarehouseId = warehouseTransfer.SourceWarehouseId;
        TargetWarehouseId = warehouseTransfer.TargetWarehouseId;
        ProductId = warehouseTransfer.ProductId;
    }
}