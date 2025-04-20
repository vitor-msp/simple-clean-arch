namespace SimpleCleanArch.Domain.Contract;

public interface IInventory
{
    public int WarehouseId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}