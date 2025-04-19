namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IBaseRepository
{
    Task Commit();
}