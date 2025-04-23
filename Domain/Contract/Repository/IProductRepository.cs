namespace SimpleCleanArch.Domain.Contract.Repository;

public interface IProductRepository : IBaseRepository
{
    Task<IProduct?> GetById(int id);
    Task<IProduct?> GetByName(string name);
    Task<int> Create(IProduct product);
    Task Update(IProduct product);
    Task Delete(IProduct product);
}