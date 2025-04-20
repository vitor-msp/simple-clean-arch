using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Repository.Schema;

[Table("warehouse_transfer_details")]
public class WarehouseTransferDetailsSchema
{
    [Key, ForeignKey("WarehouseTransfer"), Column("warehouse_transfer_id")]
    public int WarehouseTransferId { get; set; }

    public WarehouseTransferSchema WarehouseTransfer { get; set; }

    [Column("product_quantity")]
    public int ProductQuantity { get; set; }

    public WarehouseTransferDetailsSchema() { }

    public WarehouseTransferDetailsSchema(IWarehouseTransfer warehouseTransfer)
    {
        Hydrate(warehouseTransfer);
    }

    public void Update(IWarehouseTransfer warehouseTransfer)
    {
        Hydrate(warehouseTransfer);
    }

    private void Hydrate(IWarehouseTransfer warehouseTransfer)
    {
        ProductQuantity = warehouseTransfer.ProductQuantity;
    }
}