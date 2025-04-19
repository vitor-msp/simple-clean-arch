namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouseTransfer
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public Guid SourceWarehouseId { get; init; }
    public Guid TargetWarehouseId { get; init; }
    public Guid ProductId { get; init; }
    public int ProductQuantity { get; init; }
}