using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Contract;

public class ProductVariant : IProductVariant
{
    public long Id { get; }
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

    public static IProductVariant Rebuild(long id, DateTime createdAt, Color color, Size size)
        => new ProductVariant(id, createdAt)
        {
            Color = color,
            Size = size
        };

    public object Clone()
    {
        var variant = Rebuild(Id, CreatedAt, Color, Size);
        variant.Product = Product;
        return variant;
    }

    private static string FormatSkuText(string text)
        => text.ToLower().Replace(" ", "_");
}