using SimpleCleanArch.Domain.Contract;

namespace SimpleCleanArch.Domain.Entities;

public class WarehouseTransfer : IWarehouseTransfer
{
    public int Id { get; }
    public required int SourceWarehouseId { get; init; }
    public required int TargetWarehouseId { get; init; }
    public required int ProductId { get; init; }
    public required int ProductQuantity { get; init; }

    public WarehouseTransfer()
    {
        Id = default;
    }

    private WarehouseTransfer(int id)
    {
        Id = id;
    }

    public static IWarehouseTransfer Rebuild(int id, int sourceWarehouseId, int targetWarehouseId, int productId, int productQuantity)
        => new WarehouseTransfer(id)
        {
            SourceWarehouseId = sourceWarehouseId,
            TargetWarehouseId = targetWarehouseId,
            ProductId = productId,
            ProductQuantity = productQuantity,
        };
}