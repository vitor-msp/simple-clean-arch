namespace SimpleCleanArch.Repository.Implementation;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleCleanArch.Domain.Contract.Repository;

public class Transaction : ITransaction
{
    public required IDbContextTransaction RealTransaction { get; init; }

    public async Task Commit() => await RealTransaction.CommitAsync();

    public async ValueTask DisposeAsync() => await RealTransaction.DisposeAsync();
}