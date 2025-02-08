using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Implementations;

public class ProductsRepositoryMemory : IProductsRepository
{
    private readonly List<Product> _products = [];

    public Task<Product?> Get(long id)
        => Task.FromResult(_products.Find(p => p.Id == id));

    public async Task Create(Product product)
    {
        _products.Add(product);
    }

    public async Task Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index == -1) throw new Exception($"Product id {product.Id} not found.");
        _products[index] = product;
    }

    public async Task Delete(Product product)
    {
        _products.Remove(product);
    }

    public async Task Commit() { }
}