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
            details: new WarehouseDetailsDto(
                City: "belo horizonte"
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
        Assert.Null(warehouse.Details.City);
        Assert.Equal(warehouse, warehouse.Details.Warehouse);
    }

    [Fact]
    public void RebuildWarehouse_Success()
    {
        var warehouse = Warehouse.Rebuild(
            name: "my-warehouse",
            description: "my description",
            id: 1,
            details: new WarehouseDetailsDto(
                City: "belo horizonte"
            )
        );
        Assert.Equal(1, warehouse.Id);
        Assert.Equal("my-warehouse", warehouse.Name);
        Assert.Equal("my description", warehouse.Description);
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