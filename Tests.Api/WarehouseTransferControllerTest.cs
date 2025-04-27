namespace SimpleCleanArch.Tests.Api;

[Collection("Tests.Api")]
public class WarehouseTransferControllerTest : BaseControllerTest
{
    protected override async Task CleanDatabase(AppDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_transfers;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_transfer_details;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM products;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM product_variants;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouses;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_details;");
    }

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
        };
        warehouse1.Details = new WarehouseDetailsSchema()
        {
            CreatedAt = DateTime.UtcNow,
            City = "belo horizonte",
            Warehouse = warehouse1,
        };
        var warehouse2 = new WarehouseSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse 2",
        };
        warehouse2.Details = new WarehouseDetailsSchema()
        {
            CreatedAt = DateTime.UtcNow,
            City = "belo horizonte",
            Warehouse = warehouse2,
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
        Assert.NotNull(warehouseTransferSchema.Details);
        Assert.Equal(2, warehouseTransferSchema.Details.ProductQuantity);
    }
}