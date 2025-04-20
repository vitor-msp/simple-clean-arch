namespace SimpleCleanArch.Domain.Contract;

public interface IWarehouseTransfer
{
    public int Id { get; }
    public int SourceWarehouseId { get; init; }
    public int TargetWarehouseId { get; init; }
    public int ProductId { get; init; }
    public int ProductQuantity { get; init; }
}