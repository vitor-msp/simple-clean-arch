namespace SimpleCleanArch.Tests.Api;

public class WarehouseTransferControllerTest
{
    private static (WarehouseTransferController controller, AppDbContext context) MakeSut()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
        var context = new AppDbContext(contextOptions);
        context.Database.EnsureCreatedAsync();
        var createWarehouse = new CreateWarehouseTransfer(
            new WarehouseTransferRepositorySqlite(context),
            new ProductRepositorySqlite(context),
            new WarehouseRepositorySqlite(context)
        );
        var controller = new WarehouseTransferController(createWarehouse);
        return (controller, context);
    }

    [Fact]
    public async Task CreateWarehouseTransfer_Success()
    {
        var (controller, context) = MakeSut();
        var sourceWarehouseId = Guid.NewGuid();
        var targetWarehouseId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        await context.Warehouses.AddAsync(new WarehouseSchema()
        {
            Id = sourceWarehouseId,
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse 1",
            Details = new WarehouseDetailsSchema()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                City = "belo horizonte"
            }
        });
        await context.Warehouses.AddAsync(new WarehouseSchema()
        {
            Id = targetWarehouseId,
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse 2",
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
        var input = new CreateWarehouseTransferInput()
        {
            SourceWarehouseId = sourceWarehouseId,
            TargetWarehouseId = targetWarehouseId,
            ProductId = productId,
            ProductQuantity = 2,
        };
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateWarehouseTransferOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.WarehouseTransferId);
        var warehouseTransferSchema = await context.WarehouseTransfers.FindAsync(outputContent.WarehouseTransferId);
        Assert.NotNull(warehouseTransferSchema);
        Assert.Equal(sourceWarehouseId, warehouseTransferSchema.SourceWarehouseId);
        Assert.Equal(targetWarehouseId, warehouseTransferSchema.TargetWarehouseId);
        Assert.Equal(productId, warehouseTransferSchema.ProductId);
        Assert.Equal(2, warehouseTransferSchema.ProductQuantity);
    }
}