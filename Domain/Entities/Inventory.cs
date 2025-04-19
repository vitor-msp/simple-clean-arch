using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Inventory : IInventory
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required Guid WarehouseId { get; init; }
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }

    public Inventory()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    private Inventory(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static IInventory Rebuild(Guid id, DateTime createdAt, Guid warehouseId, Guid productId, int quantity)
        => new Inventory(id, createdAt)
        {
            WarehouseId = warehouseId,
            ProductId = productId,
            Quantity = quantity
        };
}