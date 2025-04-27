namespace SimpleCleanArch.Tests.Api;

[Collection("Tests.Api")]
public class InventoryControllerTest : BaseControllerTest
{
    private readonly InventoryController _controller;
    private readonly AppDbContext _context;

    public InventoryControllerTest() : base()
    {
        (_controller, _context) = MakeSut();
    }

    private (InventoryController controller, AppDbContext context) MakeSut()
    {
        var _context = CreateContext();
        var createInventory = new CreateInventory(
            new InventoryRepositorySqlite(_context),
            new ProductRepositorySqlite(_context),
            new WarehouseRepositorySqlite(_context)
        );
        var _controller = new InventoryController(createInventory);
        return (_controller, _context);
    }

    protected override async Task CleanDatabase()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM inventories;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM products;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM product_variants;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouses;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_details;");
    }

    [Fact]
    public async Task PostInventory_Success()
    {
        var warehouse = new WarehouseSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "warehouse",
        };
        warehouse.Details = new WarehouseDetailsSchema()
        {
            CreatedAt = DateTime.UtcNow,
            City = "belo horizonte",
            Warehouse = warehouse,
        };
        var product = new ProductSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "my-product",
            Price = 10
        };
        await _context.Warehouses.AddAsync(warehouse);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        var input = new CreateInventoryInput()
        {
            WarehouseId = warehouse.Id,
            ProductId = product.Id,
            Quantity = 2,
        };
        var output = await _controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateInventoryOutput>(outputResult.Value);
        Assert.NotNull(outputContent.InventoryId);
        Assert.Equal(warehouse.Id, outputContent.InventoryId.WarehouseId);
        Assert.Equal(product.Id, outputContent.InventoryId.ProductId);
        var inventorySchema = await _context.Inventories.FindAsync(outputContent.InventoryId.WarehouseId, outputContent.InventoryId.ProductId);
        Assert.NotNull(inventorySchema);
        Assert.Equal(warehouse.Id, inventorySchema.WarehouseId);
        Assert.Equal(product.Id, inventorySchema.ProductId);
        Assert.Equal(2, inventorySchema.Quantity);
    }
}