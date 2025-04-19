namespace SimpleCleanArch.Tests.Domain;

public class WarehouseTransferTest
{
    [Fact]
    public void CreateWarehouseTransfer_Success()
    {
        var warehouseTransfer = new WarehouseTransfer()
        {
            SourceWarehouseId = 1,
            TargetWarehouseId = 2,
            ProductId = 1,
            ProductQuantity = 2,
        };
        Assert.NotEqual(default, warehouseTransfer.CreatedAt);
    }

    [Fact]
    public void RebuildWarehouseTransfer_Success()
    {
        var createdAt = DateTime.Now;
        var warehouseTransfer = WarehouseTransfer.Rebuild(
            id: 1,
            createdAt: createdAt,
            sourceWarehouseId: 1,
            targetWarehouseId: 2,
            productId: 1,
            productQuantity: 2
        );
        Assert.Equal(1, warehouseTransfer.Id);
        Assert.Equal(createdAt, warehouseTransfer.CreatedAt);
        Assert.Equal(1, warehouseTransfer.SourceWarehouseId);
        Assert.Equal(2, warehouseTransfer.TargetWarehouseId);
        Assert.Equal(1, warehouseTransfer.ProductId);
        Assert.Equal(2, warehouseTransfer.ProductQuantity);
    }
}