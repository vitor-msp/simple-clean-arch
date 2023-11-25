using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Domain.Contract;

public interface IProductsRepository
{
    void Save(Product product);
    Product? Get(long id);
    void Delete(Product product);
}