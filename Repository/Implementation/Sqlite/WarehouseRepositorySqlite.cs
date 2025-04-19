using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class WarehouseRepositorySqlite(AppDbContext database) : BaseRepositorySqlite(database), IWarehouseRepository
{
    private readonly AppDbContext _database = database;

    public async Task<IWarehouse?> GetById(Guid id)
    {
        var warehouseSchema = await _database.Warehouses.FindAsync(id);
        if (warehouseSchema is null) return null;
        return warehouseSchema.GetEntity();
    }

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

    public async Task Delete(IWarehouse warehouse)
    {
        var warehouseSchema = await _database.Warehouses.FindAsync(warehouse.Id)
            ?? throw new Exception($"Warehouse id {warehouse.Id} not found.");
        _database.Warehouses.Remove(warehouseSchema);
    }

    public async Task Update(IWarehouse warehouse)
    {
        var warehouseSchema = await _database.Warehouses.FindAsync(warehouse.Id)
            ?? throw new Exception($"Warehouse id {warehouse.Id} not found.");
        warehouseSchema.Update(warehouse);
    }
}
