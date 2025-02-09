using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Repository.Implementation;

public class ProductRepositoryMemory : IProductRepository
{
    private readonly List<IProduct> _products = [];

    public Task<IProduct?> Get(long id)
        => Task.FromResult(_products.Find(p => p.Id == id));

    public async Task Create(IProduct product)
    {
        _products.Add(product);
    }

    public async Task Update(IProduct product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index == -1) throw new Exception($"Product id {product.Id} not found.");
        _products[index] = product;
    }

    public async Task Delete(IProduct product)
    {
        _products.Remove(product);
    }

    public async Task Commit() { }
}