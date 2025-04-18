using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Context;

public class WarehouseContext(DbContextOptions<WarehouseContext> options) : DbContext(options)
{
    public DbSet<WarehouseSchema> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}