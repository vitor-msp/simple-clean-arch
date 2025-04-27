namespace SimpleCleanArch.Tests.Api;

public class WarehouseControllerTest : BaseControllerTest
{
    private readonly WarehouseController _controller;
    private readonly AppDbContext _context;

    public WarehouseControllerTest() : base()
    {
        (_controller, _context) = MakeSut();
    }

    private (WarehouseController _controller, AppDbContext _context) MakeSut()
    {
        var _context = CreateContext();
        var repository = new WarehouseRepository(_context);
        var createWarehouse = new CreateWarehouse(repository);
        var deleteWarehouse = new DeleteWarehouse(repository);
        var updateWarehouse = new UpdateWarehouse(repository);
        var _controller = new WarehouseController(createWarehouse, deleteWarehouse, updateWarehouse);
        return (_controller, _context);
    }

    protected override async Task CleanDatabase()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouses;");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_details;");
    }

    private static WarehouseSchema GetWarehouseSchema()
    {
        var warehouse = new WarehouseSchema()
        {
            CreatedAt = DateTime.UtcNow,
            Name = "my-warehouse",
            Description = "my warehouse",
        };
        warehouse.Details = new WarehouseDetailsSchema()
        {
            CreatedAt = DateTime.UtcNow,
            City = "belo horizonte",
            Warehouse = warehouse,
        };
        return warehouse;
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
        var output = await _controller.Post(input);
        var outputResult = Assert.IsType<CreatedAtRouteResult>(output.Result);
        var outputContent = Assert.IsType<CreateWarehouseOutput>(outputResult.Value);
        Assert.NotEqual(default, outputContent.WarehouseId);
        var warehouseSchema = await _context.Warehouses.FindAsync(outputContent.WarehouseId);
        Assert.NotNull(warehouseSchema);
        Assert.NotEqual(default, warehouseSchema.CreatedAt);
        Assert.Equal("my-warehouse", warehouseSchema.Name);
        Assert.Equal("my warehouse description", warehouseSchema.Description);
        Assert.NotNull(warehouseSchema.Details);
        Assert.NotEqual(default, warehouseSchema.Details.CreatedAt);
        Assert.Equal("belo horizonte", warehouseSchema.Details.City);
    }

    [Fact]
    public async Task DeleteWarehouse_Success()
    {
        var warehouseSchema = GetWarehouseSchema();
        await _context.Warehouses.AddAsync(warehouseSchema);
        await _context.SaveChangesAsync();
        var output = await _controller.Delete(warehouseSchema.Id);
        Assert.IsType<NoContentResult>(output);
        var deletedWarehouseSchema = await _context.Warehouses.FindAsync(warehouseSchema.Id);
        Assert.Null(deletedWarehouseSchema);
    }

    [Fact]
    public async Task UpdateWarehouse_Success()
    {
        var warehouseSchema = GetWarehouseSchema();
        await _context.Warehouses.AddAsync(warehouseSchema);
        await _context.SaveChangesAsync();
        var input = new UpdateWarehouseInput()
        {
            Description = "my warehouse description edited",
            Details = new UpdateWarehouseInput.WarehouseDetails()
            {
                City = "sao paulo"
            }
        };
        var output = await _controller.Patch(warehouseSchema.Id, input);
        Assert.IsType<NoContentResult>(output);
        warehouseSchema = await _context.Warehouses.FindAsync(warehouseSchema.Id);
        Assert.NotNull(warehouseSchema);
        Assert.NotEqual(default, warehouseSchema.CreatedAt);
        Assert.Equal("my-warehouse", warehouseSchema.Name);
        Assert.Equal("my warehouse description edited", warehouseSchema.Description);
        Assert.NotNull(warehouseSchema.Details);
        Assert.NotEqual(default, warehouseSchema.Details.CreatedAt);
        Assert.Equal("sao paulo", warehouseSchema.Details.City);
    }
}