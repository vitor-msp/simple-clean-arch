using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Entities;

public class ProductVariant : IProductVariant
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required Color Color { get; init; }
    public required Size Size { get; init; }
    public IProduct? Product { get; set; }
    public string Sku
    {
        get
        {
            if (Product is null)
                throw new Exception("Product is not setted.");
            var name = FormatSkuText(Product.Name);
            var color = FormatSkuText(Color.ToString());
            var size = FormatSkuText(Size.ToString());
            return $"{name}-{color}-{size}";
        }
    }
    public string? Description { get; set; }

    public ProductVariant()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    private ProductVariant(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public static IProductVariant Rebuild(Guid id, DateTime createdAt, Color color, Size size, string? description)
        => new ProductVariant(id, createdAt)
        {
            Color = color,
            Size = size,
            Description = description
        };

    public object Clone()
    {
        var variant = Rebuild(Id, CreatedAt, Color, Size, Description);
        variant.Product = Product;
        return variant;
    }

    private static string FormatSkuText(string text)
        => text.ToLower().Replace(" ", "_");
}