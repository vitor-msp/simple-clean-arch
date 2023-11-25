using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Repository.Database.Schema;

public class ProductSchema
{
    public long Id { get; set; }
    public string Description { get; set; } = "";
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; } = "";

    public ProductSchema() { }

    public ProductSchema(ProductFields fields)
    {
        Hydrate(fields);
    }

    public Product GetEntity()
    {
        return new Product(ProductFields.Rebuild(Id, Description, Price, CreatedAt, Category));
    }

    public void Hydrate(ProductFields fields)
    {
        Id = fields.Id;
        Description = fields.Description;
        Price = fields.Price;
        CreatedAt = fields.CreatedAt;
        Category = fields.Category;
    }
}