using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

public class InventorySchema
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public InventorySchema() { }

    public InventorySchema(IInventory inventory)
    {
        Id = inventory.Id;
        CreatedAt = inventory.CreatedAt;
        WarehouseId = inventory.WarehouseId;
        ProductId = inventory.ProductId;
        Quantity = inventory.Quantity;
    }

    public IInventory GetEntity()
        => Inventory.Rebuild(Id, CreatedAt, WarehouseId, ProductId, Quantity);
}