using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;

namespace SimpleCleanArch.Repository.Implementation;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
public class ProductRepositoryMemory : IProductRepository
{
    private readonly List<IProduct> _products = [];

    public Task<IProduct?> Get(int id)
        => Task.FromResult(_products.Find(p => p.Id == id));

    public async Task<int> Create(IProduct product)
    {
        _products.Add(product);
        return 1;
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
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously