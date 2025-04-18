namespace SimpleCleanArch.Tests.Domain;

public class WarehouseDetailsTest
{
    [Fact]
    public void CreateWarehouseDetails_Success()
    {
        var warehouseDetails = new WarehouseDetails()
        {
            City = "belo horizonte",
        };
        Assert.NotEqual(default, warehouseDetails.Id);
        Assert.NotEqual(default, warehouseDetails.CreatedAt);
    }

    [Fact]
    public void RebuildWarehouseDetails_Success()
    {
        var id = Guid.NewGuid();
        var createdAt = DateTime.Now;
        var warehouse = WarehouseDetails.Rebuild(
            city: "belo horizonte",
            id: id,
            createdAt: createdAt
        );
        Assert.Equal(id, warehouse.Id);
        Assert.Equal(createdAt, warehouse.CreatedAt);
        Assert.Equal("belo horizonte", warehouse.City);
    }
}