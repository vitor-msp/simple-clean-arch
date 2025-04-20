namespace SimpleCleanArch.Tests.Api;

public class InventoryControllerTest : BaseControllerTest
{
    private async Task<(InventoryController controller, AppDbContext context)> MakeSut()
    {
        var context = await CreateContext();
        var createInventory = new CreateInventory(
            new InventoryRepositorySqlite(context),
            new ProductRepositorySqlite(context),
            new WarehouseRepositorySqlite(context)
        );
        var controller = new InventoryController(createInventory);
        return (controller, context);
    }

    [Fact]
    public async Task PostInventory_Success()
    {
        var (controller, context) = await MakeSut();
        var warehouse = new WarehouseSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse",
            Details = new WarehouseDetailsSchema()
            {
                CreatedAt = DateTime.Now,
                City = "belo horizonte"
            }
        };
        var product = new ProductSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "my-product",
            Price = 10
        };
        await context.Warehouses.AddAsync(warehouse);
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        var input = new CreateInventoryInput()
        {
            WarehouseId = warehouse.Id,
            ProductId = product.Id,
            Quantity = 2,
        };
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateInventoryOutput>(outputResult.Value);
        Assert.NotNull(outputContent.InventoryId);
        Assert.Equal(warehouse.Id, outputContent.InventoryId.WarehouseId);
        Assert.Equal(product.Id, outputContent.InventoryId.ProductId);
        var inventorySchema = await context.Inventories.FindAsync(outputContent.InventoryId.WarehouseId, outputContent.InventoryId.ProductId);
        Assert.NotNull(inventorySchema);
        Assert.Equal(warehouse.Id, inventorySchema.WarehouseId);
        Assert.Equal(product.Id, inventorySchema.ProductId);
        Assert.Equal(2, inventorySchema.Quantity);
    }
}