using System.ComponentModel.DataAnnotations.Schema;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

[Table("inventories")]
public class InventorySchema : BaseSchema<IInventory, IInventory>
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

    public override IInventory GetEntity()
        => Inventory.Rebuild(Id, CreatedAt, WarehouseId, ProductId, Quantity);

    public override void Update(IInventory inventory)
    {
        Hydrate(inventory);
    }

    private void Hydrate(IInventory inventory)
    {
        Id = inventory.Id;
        CreatedAt = inventory.CreatedAt;
        WarehouseId = inventory.WarehouseId;
        ProductId = inventory.ProductId;
        Quantity = inventory.Quantity;
    }
}