namespace SimpleCleanArch.Tests.Domain;

public class WarehouseTransferTest
{
    [Fact]
    public void RebuildWarehouseTransfer_Success()
    {
        var warehouseTransfer = WarehouseTransfer.Rebuild(
            id: 1,
            sourceWarehouseId: 1,
            targetWarehouseId: 2,
            productId: 1,
            productQuantity: 2
        );
        Assert.Equal(1, warehouseTransfer.Id);
        Assert.Equal(1, warehouseTransfer.SourceWarehouseId);
        Assert.Equal(2, warehouseTransfer.TargetWarehouseId);
        Assert.Equal(1, warehouseTransfer.ProductId);
        Assert.Equal(2, warehouseTransfer.ProductQuantity);
    }
}