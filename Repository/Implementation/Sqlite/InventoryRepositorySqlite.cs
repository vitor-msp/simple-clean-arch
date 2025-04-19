using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class InventoryRepositorySqlite(AppDbContext database) : BaseRepositorySqlite(database), IInventoryRepository
{
    private readonly AppDbContext _database = database;

    public async Task Create(IInventory inventory)
    {
        await _database.Inventories.AddAsync(new InventorySchema(inventory));
    }
}
