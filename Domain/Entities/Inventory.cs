using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Inventory : IInventory
{
    public DateTime CreatedAt { get; }
    public required int WarehouseId { get; init; }
    public required int ProductId { get; init; }
    public required int Quantity { get; init; }

    public Inventory()
    {
        CreatedAt = DateTime.Now;
    }

    private Inventory(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    public static IInventory Rebuild(DateTime createdAt, int warehouseId, int productId, int quantity)
        => new Inventory(createdAt)
        {
            WarehouseId = warehouseId,
            ProductId = productId,
            Quantity = quantity
        };
}