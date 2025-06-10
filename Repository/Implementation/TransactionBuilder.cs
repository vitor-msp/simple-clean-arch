using System.Data;
using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;

namespace SimpleCleanArch.Repository.Implementation;

public class TransactionBuilder(AppDbContext context) : ITransactionBuilder
{
    private readonly AppDbContext _context = context;

    public async Task<ITransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        var transaction = await _context.Database.BeginTransactionAsync(isolationLevel);
        return new Transaction() { RealTransaction = transaction };
    }
}