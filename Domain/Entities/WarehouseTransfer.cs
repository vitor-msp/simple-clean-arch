using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseTransfer : IWarehouseTransfer
{
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public required int SourceWarehouseId { get; init; }
    public required int TargetWarehouseId { get; init; }
    public required int ProductId { get; init; }
    public required int ProductQuantity { get; init; }

    public WarehouseTransfer()
    {
        CreatedAt = DateTime.Now;
    }

    private WarehouseTransfer(int id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static IWarehouseTransfer Rebuild(int id, DateTime createdAt, int sourceWarehouseId, int targetWarehouseId, int productId, int productQuantity)
        => new WarehouseTransfer(id, createdAt)
        {
            SourceWarehouseId = sourceWarehouseId,
            TargetWarehouseId = targetWarehouseId,
            ProductId = productId,
            ProductQuantity = productQuantity,
        };
}