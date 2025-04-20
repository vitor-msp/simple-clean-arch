using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

[Table("inventories")]
[PrimaryKey(nameof(WarehouseId), nameof(ProductId))]
public class InventorySchema : BaseSchema<IInventory, IInventory>
{
    [ForeignKey("Warehouse"), Column("warehouse_id")]
    public int WarehouseId { get; set; }

    public WarehouseSchema Warehouse { get; set; }

    [ForeignKey("Product"), Column("product_id")]
    public int ProductId { get; set; }

    public ProductSchema Product { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    public InventorySchema() { }

    public InventorySchema(IInventory inventory)
    {
        Hydrate(inventory);
    }

    public override IInventory GetEntity()
        => Inventory.Rebuild(CreatedAt, WarehouseId, ProductId, Quantity);

    public override void Update(IInventory inventory)
    {
        Hydrate(inventory);
    }

    private void Hydrate(IInventory inventory)
    {
        CreatedAt = inventory.CreatedAt;
        WarehouseId = inventory.WarehouseId;
        ProductId = inventory.ProductId;
        Quantity = inventory.Quantity;
    }
}