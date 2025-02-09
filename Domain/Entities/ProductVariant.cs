using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Contract;

public class ProductVariant : IProductVariant
{
    // self-generated fields
    public long Id { get; }
    public string Sku
        => $"{Product.Name.ToLower()}-{Color.ToString().ToLower()}-{Size.ToString().ToLower()}";
    public DateTime CreatedAt { get; }

    // required fields
    public required IProduct Product { get; init; }
    public required Color Color { get; init; }
    public required Size Size { get; init; }

    public ProductVariant()
    {
        Id = DateTime.Now.Ticks / 1000000;
        CreatedAt = DateTime.Now;
    }

    private ProductVariant(long id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static IProductVariant Rebuild(long id, DateTime createdAt,
        IProduct product, Color color, Size size
    )
        => new ProductVariant(id, createdAt)
        {
            Product = product,
            Color = color,
            Size = size
        };
}