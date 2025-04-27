namespace SimpleCleanArch.Tests.Api;

public abstract class BaseTest : IAsyncDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<AppDbContext> _contextOptions;

    protected BaseTest()
    {
        _connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=simplecleanarch");
        // _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connection).Options;
        // _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;
    }

    protected async Task<AppDbContext> CreateContext()
    {
        var context = new AppDbContext(_contextOptions, inTest: true);
        await context.Database.EnsureCreatedAsync();
        return context;
    }

    public async ValueTask DisposeAsync() => await _connection.DisposeAsync();
}