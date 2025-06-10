namespace SimpleCleanArch.Tests.Api;

public abstract class BaseControllerTest : IAsyncLifetime
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<AppDbContext> _contextOptions;

    protected BaseControllerTest()
    {
        _connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=simplecleanarch;Include Error Detail=true");
        // _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connection).Options;
        // _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;
    }

    protected AppDbContext CreateContext()
    {
        var context = new AppDbContext(_contextOptions, inTest: true);
        context.Database.EnsureCreated();
        return context;
    }

    public async Task InitializeAsync() => await CleanDatabase();

    public async Task DisposeAsync() => await Task.CompletedTask;

    protected abstract Task CleanDatabase();
}