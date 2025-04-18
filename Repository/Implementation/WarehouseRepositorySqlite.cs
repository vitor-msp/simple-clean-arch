using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class WarehouseRepositorySqlite(WarehouseContext database) : IWarehouseRepository
{
    private readonly WarehouseContext _database = database;

    public async Task<IWarehouse?> GetByName(string name)
    {
        var warehouseSchema = await _database.Warehouses.FirstOrDefaultAsync(warehouse => warehouse.Name.Equals(name));
        if (warehouseSchema is null) return null;
        return warehouseSchema.GetEntity();
    }

    public async Task Create(IWarehouse warehouse)
    {
        await _database.Warehouses.AddAsync(new WarehouseSchema(warehouse));
    }

    public async Task Commit()
    {
        await _database.SaveChangesAsync();
    }
}
