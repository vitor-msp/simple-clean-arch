using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class InventoryRepositorySqlite(AppDbContext database) : BaseRepositorySqlite(database), IInventoryRepository
{
    private readonly AppDbContext _database = database;

    public async Task<int> Create(IInventory inventory)
    {
        var schema = new InventorySchema(inventory);
        await _database.Inventories.AddAsync(schema);
        await Commit();
        return schema.Id;
    }
}
