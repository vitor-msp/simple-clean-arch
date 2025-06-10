namespace SimpleCleanArch.Domain.Contract.Repository;

public interface ITransaction : IAsyncDisposable
{
    public Task Commit();
}