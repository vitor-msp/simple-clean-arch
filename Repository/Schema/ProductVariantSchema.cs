using SimpleCleanArch.Domain.ValueObjects;
using SimpleCleanArch.Repository.Schema;

namespace SimpleCleanArch.Domain.Contract;

public class ProductVariantSchema
{
    public long Id { get; set; }
    public string Sku { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public required ProductSchema Product { get; set; }
    public Color Color { get; set; }
    public Size Size { get; set; }

    public ProductVariantSchema() { }

    public ProductVariantSchema(IProductVariant variant)
    {
        Hydrate(variant);
    }

    public void Update(IProductVariant variant)
    {
        Hydrate(variant);
    }

    private void Hydrate(IProductVariant variant)
    {
        Id = variant.Id;
        Sku = variant.Sku;
        CreatedAt = variant.CreatedAt;
        Color = variant.Color;
        Size = variant.Size;
    }

    public IProductVariant GetEntity() => ProductVariant.Rebuild(Id, CreatedAt, Color, Size);
}