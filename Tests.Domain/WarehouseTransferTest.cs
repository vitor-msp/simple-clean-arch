namespace SimpleCleanArch.Tests.Domain;

public class WarehouseTransferTest
{
    [Fact]
    public void CreateWarehouseTransfer_Success()
    {
        var warehouseTransfer = new WarehouseTransfer()
        {
            SourceWarehouseId = Guid.NewGuid(),
            TargetWarehouseId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductQuantity = 2,
        };
        Assert.NotEqual(default, warehouseTransfer.Id);
        Assert.NotEqual(default, warehouseTransfer.CreatedAt);
    }

    [Fact]
    public void RebuildWarehouseTransfer_Success()
    {
        var warehouseTransferId = Guid.NewGuid();
        var createdAt = DateTime.Now;
        var sourceWarehouseId = Guid.NewGuid();
        var targetWarehouseId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var warehouseTransfer = WarehouseTransfer.Rebuild(
            id: warehouseTransferId,
            createdAt: createdAt,
            sourceWarehouseId: sourceWarehouseId,
            targetWarehouseId: targetWarehouseId,
            productId: productId,
            productQuantity: 2
        );
        Assert.Equal(warehouseTransferId, warehouseTransfer.Id);
        Assert.Equal(createdAt, warehouseTransfer.CreatedAt);
        Assert.Equal(sourceWarehouseId, warehouseTransfer.SourceWarehouseId);
        Assert.Equal(targetWarehouseId, warehouseTransfer.TargetWarehouseId);
        Assert.Equal(productId, warehouseTransfer.ProductId);
        Assert.Equal(2, warehouseTransfer.ProductQuantity);
    }
}