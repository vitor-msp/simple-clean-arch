using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class Inventory : IInventory
{
    public required int WarehouseId { get; init; }
    public required int ProductId { get; init; }
    public required int Quantity { get; init; }

    public Inventory() { }

    public static IInventory Rebuild(int warehouseId, int productId, int quantity)
        => new Inventory()
        {
            WarehouseId = warehouseId,
            ProductId = productId,
            Quantity = quantity
        };
}