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

    [Fact]
    public void RebuildWarehouse_Success()
    {
        var id = Guid.NewGuid();
        var createdAt = DateTime.Now;
        var warehouse = Warehouse.Rebuild(
            name: "my-warehouse",
            description: "my description",
            id: id,
            createdAt: createdAt
        );
        Assert.Equal(id, warehouse.Id);
        Assert.Equal(createdAt, warehouse.CreatedAt);
        Assert.Equal("my-warehouse", warehouse.Name);
        Assert.Equal("my description", warehouse.Description);
    }
}