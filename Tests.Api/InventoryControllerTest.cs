namespace SimpleCleanArch.Tests.Api;

public class InventoryControllerTest
{

    private static (InventoryController controller, AppDbContext context) MakeSut()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
        var context = new AppDbContext(contextOptions);
        context.Database.EnsureCreatedAsync();
        var createInventory = new CreateInventory(
            new InventoryRepositorySqlite(context),
            new ProductRepositorySqlite(context),
            new WarehouseRepositorySqlite(context)
        );
        var controller = new InventoryController(createInventory);
        return (controller, context);
    }

    [Fact]
    public async Task CreateInventory_Success()
    {
        var (controller, context) = MakeSut();
        var warehouseId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        await context.Warehouses.AddAsync(new WarehouseSchema()
        {
            Id = warehouseId,
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse",
            Details = new WarehouseDetailsSchema()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                City = "belo horizonte"
            }
        });
        await context.Products.AddAsync(new ProductSchema()
        {
            Id = productId,
            CreatedAt = DateTime.UtcNow,
            Name = "my-product",
            Price = 10
        });
        var input = new CreateInventoryInput()
        {
            WarehouseId = warehouseId,
            ProductId = productId,
            Quantity = 2,
        };
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateInventoryOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.InventoryId);
        var inventorySchema = await context.Inventories.FindAsync(outputContent.InventoryId);
        Assert.NotNull(inventorySchema);
        Assert.Equal(warehouseId, inventorySchema.WarehouseId);
        Assert.Equal(productId, inventorySchema.ProductId);
        Assert.Equal(2, inventorySchema.Quantity);
    }
}