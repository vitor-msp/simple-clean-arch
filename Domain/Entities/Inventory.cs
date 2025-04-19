using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Inventory : IInventory
{
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public required int WarehouseId { get; init; }
    public required int ProductId { get; init; }
    public required int Quantity { get; init; }

    public Inventory()
    {
        CreatedAt = DateTime.Now;
    }

    private Inventory(int id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static IInventory Rebuild(int id, DateTime createdAt, int warehouseId, int productId, int quantity)
        => new Inventory(id, createdAt)
        {
            WarehouseId = warehouseId,
            ProductId = productId,
            Quantity = quantity
        };
}