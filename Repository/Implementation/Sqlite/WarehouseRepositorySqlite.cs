using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class WarehouseRepositorySqlite(AppDbContext database) : BaseRepositorySqlite(database), IWarehouseRepository
{
    private readonly AppDbContext _database = database;

    public async Task<IWarehouse?> GetById(int id)
    {
        var warehouseSchema = await _database.Warehouses.Include("Details")
            .FirstOrDefaultAsync(warehouse => warehouse.Id == id);
        if (warehouseSchema is null) return null;
        return warehouseSchema.GetEntity();
    }

    public async Task<IWarehouse?> GetByName(string name)
    {
        var warehouseSchema = await _database.Warehouses.Include("Details")
            .FirstOrDefaultAsync(warehouse => warehouse.Name.Equals(name));
        if (warehouseSchema is null) return null;
        return warehouseSchema.GetEntity();
    }

    public async Task<int> Create(IWarehouse warehouse)
    {
        var schema = new WarehouseSchema(warehouse);
        await _database.Warehouses.AddAsync(schema);
        await Commit();
        return schema.Id;
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
