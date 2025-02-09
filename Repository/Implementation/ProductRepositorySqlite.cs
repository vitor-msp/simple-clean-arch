using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class ProductRepositorySqlite(ProductContext database) : IProductRepository
{
    private readonly ProductContext _database = database;

    public async Task<IProduct?> Get(long id)
    {
        var product = await _database.Products.FindAsync(id);
        if (product is null) return null;
        return product.GetEntity();
    }

    public async Task Create(IProduct product)
    {
        await _database.Products.AddAsync(new ProductSchema(product));
    }

    public async Task Update(IProduct product)
    {
        var savedProduct = await _database.Products.FindAsync(product.Id)
            ?? throw new Exception($"Product id {product.Id} not found.");
        savedProduct.Hydrate(product);
    }

    public async Task Delete(IProduct product)
    {
        var savedProduct = await _database.Products.FindAsync(product.Id)
            ?? throw new Exception($"Product id {product.Id} not found.");
        _database.Products.Remove(savedProduct);
    }

    public async Task Commit()
    {
        await _database.SaveChangesAsync();
    }
}
