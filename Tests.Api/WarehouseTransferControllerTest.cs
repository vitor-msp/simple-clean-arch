namespace SimpleCleanArch.Tests.Api;

[Collection("Tests.Api")]
public class WarehouseTransferControllerTest : BaseControllerTest
{
    private readonly WarehouseTransferController _controller;
    private readonly AppDbContext _context;

    public WarehouseTransferControllerTest() : base()
    {
        (_controller, _context) = MakeSut();
    }

    private (WarehouseTransferController _controller, AppDbContext _context) MakeSut()
    {
        var _context = CreateContext();
        var createWarehouseTransfer = new CreateWarehouseTransfer(
            new WarehouseTransferRepository(_context),
            new ProductRepository(_context),
            new WarehouseRepository(_context)
        );
        var _controller = new WarehouseTransferController(createWarehouseTransfer);
        return (_controller, _context);
    }

    protected override async Task CleanDatabase()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_transfers;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_transfer_details;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM products;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM product_variants;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouses;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_details;");
    }

    [Fact]
    public async Task PostWarehouseTransfer_Success()
    {
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
        await _context.Warehouses.AddAsync(warehouse1);
        await _context.Warehouses.AddAsync(warehouse2);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        var input = new CreateWarehouseTransferInput()
        {
            SourceWarehouseId = warehouse1.Id,
            TargetWarehouseId = warehouse2.Id,
            ProductId = product.Id,
            ProductQuantity = 2,
        };
        var output = await _controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateWarehouseTransferOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.WarehouseTransferId);
        var warehouseTransferSchema = await _context.WarehouseTransfers.FindAsync(outputContent.WarehouseTransferId);
        Assert.NotNull(warehouseTransferSchema);
        Assert.Equal(warehouse1.Id, warehouseTransferSchema.SourceWarehouseId);
        Assert.Equal(warehouse2.Id, warehouseTransferSchema.TargetWarehouseId);
        Assert.Equal(product.Id, warehouseTransferSchema.ProductId);
        Assert.NotNull(warehouseTransferSchema.Details);
        Assert.Equal(2, warehouseTransferSchema.Details.ProductQuantity);
    }
}