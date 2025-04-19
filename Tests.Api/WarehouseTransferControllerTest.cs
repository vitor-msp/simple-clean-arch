namespace SimpleCleanArch.Tests.Api;

public class WarehouseTransferControllerTest : BaseControllerTest
{
    private async Task<(WarehouseTransferController controller, AppDbContext context)> MakeSut()
    {
        var context = await CreateContext();
        var createWarehouseTransfer = new CreateWarehouseTransfer(
            new WarehouseTransferRepositorySqlite(context),
            new ProductRepositorySqlite(context),
            new WarehouseRepositorySqlite(context)
        );
        var controller = new WarehouseTransferController(createWarehouseTransfer);
        return (controller, context);
    }

    [Fact]
    public async Task PostWarehouseTransfer_Success()
    {
        var (controller, context) = await MakeSut();
        var warehouse1 = new WarehouseSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse 1",
            Details = new WarehouseDetailsSchema()
            {
                CreatedAt = DateTime.Now,
                City = "belo horizonte"
            }
        };
        var warehouse2 = new WarehouseSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse 2",
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
        await context.Warehouses.AddAsync(warehouse1);
        await context.Warehouses.AddAsync(warehouse2);
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        var input = new CreateWarehouseTransferInput()
        {
            SourceWarehouseId = warehouse1.Id,
            TargetWarehouseId = warehouse2.Id,
            ProductId = product.Id,
            ProductQuantity = 2,
        };
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateWarehouseTransferOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.WarehouseTransferId);
        var warehouseTransferSchema = await context.WarehouseTransfers.FindAsync(outputContent.WarehouseTransferId);
        Assert.NotNull(warehouseTransferSchema);
        Assert.Equal(warehouse1.Id, warehouseTransferSchema.SourceWarehouseId);
        Assert.Equal(warehouse2.Id, warehouseTransferSchema.TargetWarehouseId);
        Assert.Equal(product.Id, warehouseTransferSchema.ProductId);
        Assert.Equal(2, warehouseTransferSchema.ProductQuantity);
    }
}