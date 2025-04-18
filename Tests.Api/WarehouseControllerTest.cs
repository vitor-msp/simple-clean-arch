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
        var controller = new WarehouseController(createWarehouse);
        return (controller, context);
    }

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
}