namespace SimpleCleanArch.Tests.Api;

public class WarehouseControllerTest
{
    private static (WarehouseController controller, WarehouseContext context) MakeSut()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<WarehouseContext>().UseSqlite(connection).Options;
        var context = new WarehouseContext(contextOptions);
        context.Database.EnsureCreatedAsync();
        var repository = new WarehouseRepositorySqlite(context);
        var createWarehouse = new CreateWarehouse(repository);
        var deleteWarehouse = new DeleteWarehouse(repository);
        var updateWarehouse = new UpdateWarehouse(repository);
        var controller = new WarehouseController(createWarehouse, deleteWarehouse, updateWarehouse);
        return (controller, context);
    }

    private static WarehouseSchema GetWarehouseSchema(Guid id)
        => new()
        {
            Id = id,
            CreatedAt = DateTime.Now,
            Name = "my-warehouse",
            Description = "my warehouse",
            Details = new WarehouseDetailsSchema()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                City = "belo horizonte",
            }
        };

    [Fact]
    public async Task PostWarehouse_Success()
    {
        var input = new CreateWarehouseInput()
        {
            Name = "my-warehouse",
            Description = "my warehouse description",
            Details = new CreateWarehouseInput.WarehouseDetails()
            {
                City = "belo horizonte"
            }
        };
        var (controller, context) = MakeSut();
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateWarehouseOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.WarehouseId);
        var warehouseSchema = await context.Warehouses.FindAsync(outputContent.WarehouseId);
        Assert.NotNull(warehouseSchema);
        Assert.NotEqual(default, warehouseSchema.CreatedAt);
        Assert.Equal("my-warehouse", warehouseSchema.Name);
        Assert.Equal("my warehouse description", warehouseSchema.Description);
        Assert.NotEqual(default, warehouseSchema.Details.Id);
        Assert.NotEqual(default, warehouseSchema.Details.CreatedAt);
        Assert.Equal("belo horizonte", warehouseSchema.Details.City);
    }

    [Fact]
    public async Task DeleteWarehouse_Success()
    {
        var warehouseId = Guid.NewGuid();
        var warehouseSchema = GetWarehouseSchema(warehouseId);
        var (controller, context) = MakeSut();
        await context.Warehouses.AddAsync(warehouseSchema);
        var output = await controller.Delete(warehouseId);
        Assert.IsType<NoContentResult>(output);
        var deletedWarehouseSchema = await context.Warehouses.FindAsync(warehouseId);
        Assert.Null(deletedWarehouseSchema);
    }

    [Fact]
    public async Task UpdateWarehouse_Success()
    {
        var warehouseId = Guid.NewGuid();
        var warehouseSchema = GetWarehouseSchema(warehouseId);
        var (controller, context) = MakeSut();
        await context.Warehouses.AddAsync(warehouseSchema);
        var input = new UpdateWarehouseInput()
        {
            Description = "my warehouse description edited",
            Details = new UpdateWarehouseInput.WarehouseDetails()
            {
                City = "sao paulo"
            }
        };
        var output = await controller.Patch(warehouseId, input);
        Assert.IsType<NoContentResult>(output);
        warehouseSchema = await context.Warehouses.FindAsync(warehouseId);
        Assert.NotNull(warehouseSchema);
        Assert.NotEqual(default, warehouseSchema.CreatedAt);
        Assert.Equal("my-warehouse", warehouseSchema.Name);
        Assert.Equal("my warehouse description edited", warehouseSchema.Description);
        Assert.NotEqual(default, warehouseSchema.Details.Id);
        Assert.NotEqual(default, warehouseSchema.Details.CreatedAt);
        Assert.Equal("sao paulo", warehouseSchema.Details.City);
    }
}