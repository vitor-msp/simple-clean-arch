namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IProductRepository : IBaseRepository
{
    Task<IProduct?> Get(int id);
    Task<int> Create(IProduct product);
    Task Update(IProduct product);
    Task Delete(IProduct product);
}