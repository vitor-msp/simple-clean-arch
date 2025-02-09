using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Entities;

public class Product : IProduct
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
                throw new DomainException($"Price must be between {_minPrice} and {_maxPrice}.");
            _price = value;
        }
    }

    // optional fields
    public string? Description { get; set; }
    public string? Category { get; set; }

    // control fields (not persisted)
    private static readonly double _minPrice = 0;
    private static readonly double _maxPrice = 100;

    // entities
    private readonly List<IProductVariant> _productVariants = [];

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

    public static Product Rebuild(long id, DateTime createdAt, string name,
        double price, string? description, string? category)
        => new(id, createdAt)
        {
            Name = name,
            Price = price,
            Description = description,
            Category = category
        };

    public void AddProductVariant(Color color, Size size)
    {
        _productVariants.Add(new ProductVariant()
        {
            Product = this,
            Color = color,
            Size = size
        });
    }

    public void RemoveProductVariant(string sku)
    {
        var index = _productVariants.FindIndex(variant => variant.Sku == sku);
        if (index == -1)
            throw new DomainException($"Product variant sku {sku} not found.");
        _productVariants.RemoveAt(index);
    }

    public IProductVariant GetProductVariant(string sku)
    {
        var variant = _productVariants.Find(variant => variant.Sku == sku)
            ?? throw new DomainException($"Product variant sku {sku} not found.");
        return (IProductVariant)variant.Clone();
    }

    public List<IProductVariant> ListProductVariants()
    {
        var newList = new List<IProductVariant>();
        _productVariants.ForEach(variant => newList.Add((IProductVariant)variant.Clone()));
        return newList;
    }
}