using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Domain.Contract;

public interface IProductsRepository
{
    Product? Get(long id);
    void Create(Product product);
    void Update(Product product);
    void Delete(Product product);
}