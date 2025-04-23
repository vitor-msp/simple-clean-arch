namespace SimpleCleanArch.Tests.Api;

public abstract class BaseControllerTest : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<AppDbContext> _contextOptions;

    protected BaseControllerTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;
    }

    protected async Task<AppDbContext> CreateContext()
    {
        var context = new AppDbContext(_contextOptions, useInMemoryDb: true);
        await context.Database.EnsureCreatedAsync();
        return context;
    }

    public void Dispose() => _connection.Dispose();
}