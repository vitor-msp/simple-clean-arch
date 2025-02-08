namespace SimpleCleanArch.Domain.Entities;

public class Product
{
    // self-generated fields
    public long Id { get; }
    public DateTime CreatedAt { get; }

    // required fields
    public required string Name { get; init; }
    private double _price;
    public required double Price
    {
        get { return _price; }
        set
        {
            if (value <= _minPrice || value > _maxPrice)
                throw new Exception("price must be greater than $0 and less than or equal $100");
            _price = value;
        }
    }

    // optional fields
    public string? Description { get; set; }
    public string? Category { get; set; }

    // control fields (not persisted)
    private static readonly double _minPrice = 0;
    private static readonly double _maxPrice = 100;

    public Product()
    {
        Id = DateTime.Now.Ticks / 1000000;
        CreatedAt = DateTime.Now;
    }

    private Product(long id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static Product Rebuild(long id, DateTime createdAt, string name, double price, string? description, string? category)
    {
        return new Product(id, createdAt)
        {
            Name = name,
            Price = price,
            Description = description,
            Category = category
        };
    }
}