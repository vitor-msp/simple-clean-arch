namespace SimpleCleanArch.Tests;

public class WarehouseTest
{
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
    }
}