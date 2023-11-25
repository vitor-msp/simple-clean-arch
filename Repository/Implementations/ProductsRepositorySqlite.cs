using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;
using SimpleCleanArch.Repository.Database.Context;
using SimpleCleanArch.Repository.Database.Schema;

namespace SimpleCleanArch.Repository.Implementations;

public class ProductsRepositorySqlite : IProductsRepository
{
    private readonly ProductsContext _database;

    public ProductsRepositorySqlite(ProductsContext database)
    {
        _database = database;

    }

    public void Save(Product product)
    {
        var productRegistry = _database.Products.Find(product.GetFields().Id);
        if (productRegistry == null) Add(product);
        else Update(productRegistry, product);
    }

    private void Add(Product product)
    {
        var productRegistry = new ProductSchema(product.GetFields());
        _database.Products.Add(productRegistry);
        _database.SaveChanges();
    }

    private void Update(ProductSchema productRegistry, Product product)
    {
        productRegistry.Hydrate(product.GetFields());
        _database.SaveChanges();
    }

    public Product? Get(long id)
    {
        var productRegistry = _database.Products.Find(id);
        if (productRegistry == null) return null;
        return productRegistry.GetEntity();
    }

    public void Delete(Product product)
    {
        var productRegistry = _database.Products.Find(product.GetFields().Id);
        if (productRegistry == null) throw new Exception("product not found");
        _database.Products.Remove(productRegistry);
        _database.SaveChanges();
    }
}