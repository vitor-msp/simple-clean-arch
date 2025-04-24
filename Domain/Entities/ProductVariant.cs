using SimpleCleanArch.Domain.Contract;
using SimpleCleanArch.Domain.ValueObjects;

namespace SimpleCleanArch.Domain.Entities;

public class ProductVariant : IProductVariant
{
    public int Id { get; }
    public required Color Color { get; init; }
    public required Size Size { get; init; }
    public required IProduct Product { get; init; }
    public string? Sku { get; private set; }
    public string? Description { get; set; }

    public ProductVariant()
    {
        Id = default;
    }

    private ProductVariant(ProductVariantDto variant)
    {
        Id = variant.Id ?? throw new Exception("Cannot create ProductVariant without Id.");
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
        => Rebuild(
            variant: new ProductVariantDto(Id, Color, Size, Description, Sku),
            product: Product
        );

    public IProductVariant GenerateSku()
    {
        if (Product is null)
            throw new Exception("Cannot generate sku without Product.");
        Sku = new Sku()
        {
            ProductName = Product.Name,
            Color = Color,
            Size = Size,
        }.Value;
        return this;
    }
}