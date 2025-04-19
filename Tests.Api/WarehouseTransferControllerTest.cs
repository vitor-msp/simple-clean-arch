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
        var createWarehouseTransfer = new CreateWarehouseTransfer(
            new WarehouseTransferRepositorySqlite(context),
            new ProductRepositorySqlite(context),
            new WarehouseRepositorySqlite(context)
        );
        var controller = new WarehouseTransferController(createWarehouseTransfer);
        return (controller, context);
    }

    [Fact]
    public async Task CreateWarehouseTransfer_Success()
    {
        var (controller, context) = MakeSut();
        await context.Warehouses.AddAsync(new WarehouseSchema()
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse 1",
            Details = new WarehouseDetailsSchema()
            {
                CreatedAt = DateTime.Now,
                City = "belo horizonte"
            }
        });
        await context.Warehouses.AddAsync(new WarehouseSchema()
        {
            Id = 2,
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse 2",
            Details = new WarehouseDetailsSchema()
            {
                CreatedAt = DateTime.Now,
                City = "belo horizonte"
            }
        });
        await context.Products.AddAsync(new ProductSchema()
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            Name = "my-product",
            Price = 10
        });
        await context.SaveChangesAsync();
        var input = new CreateWarehouseTransferInput()
        {
            SourceWarehouseId = 1,
            TargetWarehouseId = 2,
            ProductId = 1,
            ProductQuantity = 2,
        };
        var output = await controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateWarehouseTransferOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.WarehouseTransferId);
        var warehouseTransferSchema = await context.WarehouseTransfers.FindAsync(outputContent.WarehouseTransferId);
        Assert.NotNull(warehouseTransferSchema);
        Assert.Equal(1, warehouseTransferSchema.SourceWarehouseId);
        Assert.Equal(2, warehouseTransferSchema.TargetWarehouseId);
        Assert.Equal(1, warehouseTransferSchema.ProductId);
        Assert.Equal(2, warehouseTransferSchema.ProductQuantity);
    }
}