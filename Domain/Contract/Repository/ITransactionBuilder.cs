using System.Data;

namespace SimpleCleanArch.Domain.Contract.Repository;

public interface ITransactionBuilder
{
    public Task<ITransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}