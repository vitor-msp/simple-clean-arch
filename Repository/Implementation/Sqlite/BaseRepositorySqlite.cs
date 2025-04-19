using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;

namespace SimpleCleanArch.Repository.Implementation;

public abstract class BaseRepositorySqlite(AppDbContext database) : IBaseRepository
{
    private readonly AppDbContext _database = database;

    public async Task Commit()
    {
        await _database.SaveChangesAsync();
    }
}
