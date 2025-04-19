using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseTransfer : IWarehouseTransfer
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required Guid SourceWarehouseId { get; init; }
    public required Guid TargetWarehouseId { get; init; }
    public required Guid ProductId { get; init; }
    public required int ProductQuantity { get; init; }

    public WarehouseTransfer()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    private WarehouseTransfer(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static IWarehouseTransfer Rebuild(Guid id, DateTime createdAt, Guid sourceWarehouseId, Guid targetWarehouseId, Guid productId, int productQuantity)
        => new WarehouseTransfer(id, createdAt)
        {
            SourceWarehouseId = sourceWarehouseId,
            TargetWarehouseId = targetWarehouseId,
            ProductId = productId,
            ProductQuantity = productQuantity,
        };
}