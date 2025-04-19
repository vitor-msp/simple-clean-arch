using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class WarehouseTransferRepositorySqlite(AppDbContext database) : IWarehouseTransferRepository
{
    private readonly AppDbContext _database = database;


    public async Task Create(IWarehouseTransfer warehouseTransfer)
    {
        await _database.WarehouseTransfers.AddAsync(new WarehouseTransferSchema(warehouseTransfer));
    }

    public async Task Commit()
    {
        await _database.SaveChangesAsync();
    }
}
