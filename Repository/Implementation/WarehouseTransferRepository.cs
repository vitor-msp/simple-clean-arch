using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class WarehouseTransferRepository(AppDbContext database) : BaseRepository(database), IWarehouseTransferRepository
{
    private readonly AppDbContext _database = database;

    public async Task<int> Create(IWarehouseTransfer warehouseTransfer)
    {
        var schema = new WarehouseTransferSchema(warehouseTransfer);
        await _database.WarehouseTransfers.AddAsync(schema);
        await Commit();
        return schema.Id;
    }
}
