namespace SimpleCleanArch.Tests.Domain;

public class WarehouseTest
{
    private static Warehouse GetWarehouse()
        => new()
        {
            Name = "my-warehouse",
            Description = "my description",
        };

    private static IWarehouse GetWarehouseWithDetails()
        => Warehouse.Rebuild(
            name: "my-warehouse",
            description: "my description",
            id: Guid.NewGuid(),
            createdAt: DateTime.Now,
            details: WarehouseDetails.Rebuild(
                city: "belo horizonte",
                id: Guid.NewGuid(),
                createdAt: DateTime.Now
            )
        );

    [Fact]
    public void CreateWarehouse_Success()
    {
        var warehouse = new Warehouse()
        {
            Name = "my-warehouse",
            Description = "my description",
        };
        Assert.NotEqual(default, warehouse.Id);
        Assert.NotEqual(default, warehouse.CreatedAt);
        Assert.NotEqual(default, warehouse.Details.Id);
        Assert.Null(warehouse.Details.City);
        Assert.Equal(warehouse, warehouse.Details.Warehouse);
    }

    [Fact]
    public void RebuildWarehouse_Success()
    {
        var warehouseId = Guid.NewGuid();
        var warehouseDetailsId = Guid.NewGuid();
        var createdAt = DateTime.Now;
        var warehouse = Warehouse.Rebuild(
            name: "my-warehouse",
            description: "my description",
            id: warehouseId,
            createdAt: createdAt,
            details: WarehouseDetails.Rebuild(
                city: "belo horizonte",
                id: warehouseDetailsId,
                createdAt: createdAt
            )
        );
        Assert.Equal(warehouseId, warehouse.Id);
        Assert.Equal(createdAt, warehouse.CreatedAt);
        Assert.Equal("my-warehouse", warehouse.Name);
        Assert.Equal("my description", warehouse.Description);
        Assert.Equal(warehouseDetailsId, warehouse.Details.Id);
        Assert.Equal(createdAt, warehouse.Details.CreatedAt);
        Assert.Equal("belo horizonte", warehouse.Details.City);
        Assert.Equal(warehouse, warehouse.Details.Warehouse);
    }

    [Fact]
    public void EnsureWarehouseDetailsEncapsulation()
    {
        var warehouse = GetWarehouseWithDetails();
        warehouse.Details.City = "sao paulo";
        Assert.Equal("belo horizonte", warehouse.Details.City);
    }

    [Fact]
    public void UpdateWarehouseDetails_Success()
    {
        var warehouse = GetWarehouse();
        var details = new WarehouseDetails
        {
            City = "sao paulo",
        };
        warehouse.UpdateDetails(details);
        Assert.Equal("sao paulo", warehouse.Details.City);
        Assert.Equal(warehouse, warehouse.Details.Warehouse);
    }
}