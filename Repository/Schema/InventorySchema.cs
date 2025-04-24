using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

[Table("inventories")]
[PrimaryKey(nameof(WarehouseId), nameof(ProductId))]
public class InventorySchema : BaseSchema, IRegenerableSchema<IInventory>
{
    [Column("warehouse_id")]
    public int WarehouseId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    public InventorySchema() { }

    public InventorySchema(IInventory inventory)
    {
        Hydrate(inventory);
    }

    public IInventory GetEntity()
        => Inventory.Rebuild(WarehouseId, ProductId, Quantity);

    private void Hydrate(IInventory inventory)
    {
        WarehouseId = inventory.WarehouseId;
        ProductId = inventory.ProductId;
        Quantity = inventory.Quantity;
    }
}