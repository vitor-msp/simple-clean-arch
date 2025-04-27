namespace SimpleCleanArch.Tests.Api;

public class WarehouseControllerTest : BaseControllerTest
{
    protected override async Task CleanDatabase(AppDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouses;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_details;");
    }

    private async Task<(WarehouseController controller, AppDbContext context)> MakeSut()
    {
        var context = await CreateContext();
        var repository = new WarehouseRepositorySqlite(context);
        var createWarehouse = new CreateWarehouse(repository);
        var deleteWarehouse = new DeleteWarehouse(repository);
        var updateWarehouse = new UpdateWarehouse(repository);
        var controller = new WarehouseController(createWarehouse, deleteWarehouse, updateWarehouse);
        return (controller, context);
    }

    private static WarehouseSchema GetWarehouseSchema()
    {
        var warehouse = new WarehouseSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "my-warehouse",
            Description = "my warehouse",
        };
        warehouse.Details = new WarehouseDetailsSchema()
        {
            CreatedAt = DateTime.UtcNow,
            City = "belo horizonte",
            Warehouse = warehouse,
        };
        return warehouse;
    }

    [Fact]
    public async Task PostWarehouse_Success()
    {
        var (controller, context) = await MakeSut();
        var input = new CreateWarehouseInput()
        {
            Name = "my-warehouse",
            Description = "my warehouse description",
            Details = new CreateWarehouseInput.WarehouseDetails()
            {
                City = "belo horizonte"
            }
        };
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateWarehouseOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.WarehouseId);
        var warehouseSchema = await context.Warehouses.FindAsync(outputContent.WarehouseId);
        Assert.NotNull(warehouseSchema);
        Assert.NotEqual(default, warehouseSchema.CreatedAt);
        Assert.Equal("my-warehouse", warehouseSchema.Name);
        Assert.Equal("my warehouse description", warehouseSchema.Description);
        Assert.NotNull(warehouseSchema.Details);
        Assert.NotEqual(default, warehouseSchema.Details.CreatedAt);
        Assert.Equal("belo horizonte", warehouseSchema.Details.City);
    }

    [Fact]
    public async Task DeleteWarehouse_Success()
    {
        var (controller, context) = await MakeSut();
        var warehouseSchema = GetWarehouseSchema();
        await context.Warehouses.AddAsync(warehouseSchema);
        await context.SaveChangesAsync();
        var output = await controller.Delete(warehouseSchema.Id);
        Assert.IsType<NoContentResult>(output);
        var deletedWarehouseSchema = await context.Warehouses.FindAsync(warehouseSchema.Id);
        Assert.Null(deletedWarehouseSchema);
    }

    [Fact]
    public async Task UpdateWarehouse_Success()
    {
        var (controller, context) = await MakeSut();
        var warehouseSchema = GetWarehouseSchema();
        await context.Warehouses.AddAsync(warehouseSchema);
        await context.SaveChangesAsync();
        var input = new UpdateWarehouseInput()
        {
            Description = "my warehouse description edited",
            Details = new UpdateWarehouseInput.WarehouseDetails()
            {
                City = "sao paulo"
            }
        };
        var output = await controller.Patch(warehouseSchema.Id, input);
        Assert.IsType<NoContentResult>(output);
        warehouseSchema = await context.Warehouses.FindAsync(warehouseSchema.Id);
        Assert.NotNull(warehouseSchema);
        Assert.NotEqual(default, warehouseSchema.CreatedAt);
        Assert.Equal("my-warehouse", warehouseSchema.Name);
        Assert.Equal("my warehouse description edited", warehouseSchema.Description);
        Assert.NotNull(warehouseSchema.Details);
        Assert.NotEqual(default, warehouseSchema.Details.CreatedAt);
        Assert.Equal("sao paulo", warehouseSchema.Details.City);
    }
}