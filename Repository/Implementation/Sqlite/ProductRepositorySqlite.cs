using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class ProductRepositorySqlite(AppDbContext database) : BaseRepositorySqlite(database), IProductRepository
{
    private readonly AppDbContext _database = database;

    public async Task<IProduct?> Get(Guid id)
    {
        var productSchema = await _database.Products.FindAsync(id);
        if (productSchema is null) return null;
        return productSchema.GetEntity();
    }

    public async Task Create(IProduct product)
    {
        await _database.Products.AddAsync(new ProductSchema(product));
    }

    public async Task Update(IProduct product)
    {
        var productSchema = await _database.Products.FindAsync(product.Id)
            ?? throw new Exception($"Product id {product.Id} not found.");
        productSchema.Update(product);
    }

    public async Task Delete(IProduct product)
    {
        var productSchema = await _database.Products.FindAsync(product.Id)
            ?? throw new Exception($"Product id {product.Id} not found.");
        _database.Products.Remove(productSchema);
    }
}
