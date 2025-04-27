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

    protected abstract Task CleanDatabase(AppDbContext context);

    public async ValueTask DisposeAsync() => await _connection.DisposeAsync();
}