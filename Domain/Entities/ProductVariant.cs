using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Entities;

public class ProductVariant : IProductVariant
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public required Color Color { get; init; }
    public required Size Size { get; init; }
    public required IProduct Product { get; init; }
    public string? Sku { get; private set; }
    public string? Description { get; set; }

    public ProductVariant()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    private ProductVariant(ProductVariantDto variant)
    {
        Id = variant.Id ?? throw new Exception("Cannot create ProductVariant without Id.");
        CreatedAt = variant.CreatedAt ?? throw new Exception("Cannot create ProductVariant without CreatedAt.");
    }

    public static IProductVariant Rebuild(ProductVariantDto variant, IProduct product)
        => new ProductVariant(variant)
        {
            Color = variant.Color ?? throw new Exception("Cannot create ProductVariant without Color."),
            Size = variant.Size ?? throw new Exception("Cannot create ProductVariant without Size."),
            Description = variant.Description,
            Sku = variant.Sku ?? throw new Exception("Cannot create ProductVariant without Sku."),
            Product = product,
        };

    public object Clone()
    {
        var variant = Rebuild(
            variant: new ProductVariantDto(Id, CreatedAt, Color, Size, Description, Sku),
            product: Product);
        return variant;
    }

    private static string FormatSkuText(string text)
        => text.ToLower().Replace(" ", "_");

    public IProductVariant GenerateSku()
    {
        if (Product is null)
            throw new Exception("Cannot generate sku without Product.");
        var name = FormatSkuText(Product.Name);
        var color = FormatSkuText(Color.ToString());
        var size = FormatSkuText(Size.ToString());
        Sku = $"{name}-{color}-{size}";
        return this;
    }
}