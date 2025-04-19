using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Context;

public class AppDbContext : DbContext
{
    private readonly bool _useInMemoryDb = false;

    public DbSet<ProductSchema> Products { get; set; }
    public DbSet<WarehouseSchema> Warehouses { get; set; }
    public DbSet<WarehouseTransferSchema> WarehouseTransfers { get; set; }
    public DbSet<InventorySchema> Inventories { get; set; }

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public AppDbContext(DbContextOptions<AppDbContext> options, bool useInMemoryDb) : base(options)
    {
        _useInMemoryDb = useInMemoryDb;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!_useInMemoryDb)
            optionsBuilder.UseSqlite();
        // optionsBuilder.UseSqlite("DataSource=../database.db");
        // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        // optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}