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
            id: 1,
            createdAt: DateTime.Now,
            details: new WarehouseDetailsDto(
                City: "belo horizonte",
                Id: 1,
                CreatedAt: DateTime.Now
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
        Assert.NotEqual(default, warehouse.CreatedAt);
        Assert.Null(warehouse.Details.City);
        Assert.Equal(warehouse, warehouse.Details.Warehouse);
    }

    [Fact]
    public void RebuildWarehouse_Success()
    {
        var createdAt = DateTime.Now;
        var warehouse = Warehouse.Rebuild(
            name: "my-warehouse",
            description: "my description",
            id: 1,
            createdAt: createdAt,
            details: new WarehouseDetailsDto(
                City: "belo horizonte",
                Id: 1,
                CreatedAt: createdAt
            )
        );
        Assert.Equal(1, warehouse.Id);
        Assert.Equal(createdAt, warehouse.CreatedAt);
        Assert.Equal("my-warehouse", warehouse.Name);
        Assert.Equal("my description", warehouse.Description);
        Assert.Equal(1, warehouse.Details.Id);
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
        var details = new WarehouseDetailsDto(City: "sao paulo");
        warehouse.UpdateDetails(details);
        Assert.Equal("sao paulo", warehouse.Details.City);
        Assert.Equal(warehouse, warehouse.Details.Warehouse);
    }
}