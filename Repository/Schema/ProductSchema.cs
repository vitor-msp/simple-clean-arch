using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Schema;

public class ProductSchema
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }

    public ProductSchema() { }

    public ProductSchema(IProduct product)
    {
        Hydrate(product);
    }

    public void Hydrate(IProduct product)
    {
        Id = product.Id;
        CreatedAt = product.CreatedAt;
        Name = product.Name;
        Price = product.Price;
        Description = product.Description;
        Category = product.Category;
    }

    public IProduct GetEntity()
        => Product.Rebuild(Id, CreatedAt, Name, Price, Description, Category);
}