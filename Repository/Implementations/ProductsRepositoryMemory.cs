using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Implementations;

public class ProductsRepositoryMemory : IProductsRepository
{
    private readonly List<Product> _products = new();

    public void Save(Product product)
    {
        _products.Add(product);
    }

    public Product? Get(long id)
    {
        return _products.Find(p => p.GetFields().Id == id);
    }
}