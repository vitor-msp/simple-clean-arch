using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Domain.Contract;

public interface IProductsRepository
{
    Task<Product?> Get(long id);
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(Product product);
    Task Commit();
}