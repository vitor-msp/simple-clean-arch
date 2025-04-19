namespace SimpleCleanArch.Domain.Contract;

public interface IInventory
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public Guid WarehouseId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
}