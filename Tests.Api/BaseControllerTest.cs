namespace SimpleCleanArch.Tests.Api;

public abstract class BaseControllerTest : IAsyncDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<AppDbContext> _contextOptions;

    protected BaseControllerTest()
    {
        _connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=test;Username=postgres;Password=simplecleanarch;Include Error Detail=true");
        // _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connection).Options;
        // _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;
    }

    protected async Task<AppDbContext> CreateContext()
    {
        var context = new AppDbContext(_contextOptions, inTest: true);
        await context.Database.EnsureCreatedAsync();
        await CleanDatabase(context);
        return context;
    }

    private static async Task CleanDatabase(AppDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("DELETE FROM employees;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM employees_projects;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM inventories;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM products;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM product_variants;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM projects;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouses;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_details;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_transfers;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM warehouse_transfer_details;");
    }

    public async ValueTask DisposeAsync() => await _connection.DisposeAsync();
}