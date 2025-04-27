using Microsoft.EntityFrameworkCore;
using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Contract.Repository;
using SimpleCleanArch.Repository.Context;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Repository.Implementation;

public class ProductRepository(AppDbContext database) : BaseRepository(database), IProductRepository
{
    private readonly AppDbContext _database = database;

    public async Task<IProduct?> GetById(int id)
    {
        var productSchema = await _database.Products
            .Include("ProductVariants").FirstOrDefaultAsync(product => product.Id == id);
        if (productSchema is null) return null;
        return productSchema.GetEntity();
    }

    public async Task<IProduct?> GetByName(string name)
    {
        var productSchema = await _database.Products
            .Include("ProductVariants").FirstOrDefaultAsync(product => product.Name.Equals(name));
        if (productSchema is null) return null;
        return productSchema.GetEntity();
    }

    public async Task<int> Create(IProduct product)
    {
        var schema = new ProductSchema(product);
        await _database.Products.AddAsync(schema);
        await Commit();
        return schema.Id;
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
