namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IProductRepository
{
    Task<IProduct?> Get(long id);
    Task Create(IProduct product);
    Task Update(IProduct product);
    Task Delete(IProduct product);
    Task Commit();
}