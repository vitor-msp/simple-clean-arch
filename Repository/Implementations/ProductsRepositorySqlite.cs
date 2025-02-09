using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;
using SimpleCleanArch.Repository.Database.Context;
using SimpleCleanArch.Repository.Database.Schema;

namespace SimpleCleanArch.Repository.Implementations;

public class ProductsRepositorySqlite(ProductsContext database) : IProductsRepository
{
    private readonly ProductsContext _database = database;

    public async Task<Product?> Get(long id)
    {
        var product = await _database.Products.FindAsync(id);
        if (product is null) return null;
        return product.GetEntity();
    }

    public async Task Create(Product product)
    {
        await _database.Products.AddAsync(new ProductSchema(product));
    }

    public async Task Update(Product product)
    {
        var savedProduct = await _database.Products.FindAsync(product.Id)
            ?? throw new Exception($"Product id {product.Id} not found.");
        savedProduct.Hydrate(product);
    }

    public async Task Delete(Product product)
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
