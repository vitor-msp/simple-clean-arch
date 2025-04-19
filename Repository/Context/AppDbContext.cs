using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductSchema> Products { get; set; }
    public DbSet<WarehouseSchema> Warehouses { get; set; }
    public DbSet<WarehouseTransferSchema> WarehouseTransfers { get; set; }
    public DbSet<InventorySchema> Inventories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        // optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}